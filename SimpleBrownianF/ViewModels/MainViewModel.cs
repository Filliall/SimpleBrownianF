﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleBrownianF.Models;
using SimpleBrownianF.Services;

namespace SimpleBrownianF.ViewModels;


public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IBrownianService _brownianService;
    private readonly Random _random = new();
    private static readonly SKColor[] s_palette =
    [
        new(30, 144, 255),  // DodgerBlue
        new(255, 69, 0),    // OrangeRed
        new(50, 205, 50),   // LimeGreen
        new(148, 0, 211),   // DarkViolet
        new(255, 215, 0),   // Gold
        new(0, 139, 139),   // DarkCyan
        new(218, 112, 214)  // Orchid
    ];

    [ObservableProperty]
    private double _initialPrice = 100;

    [ObservableProperty]
    private double _sigma = 0.2; // Volatility

    [ObservableProperty]
    private double _mean = 0.05; // Drift

    [ObservableProperty]
    private int _numDays = 252; // Number of trading days in a year

    [ObservableProperty]
    private int _numberOfSimulations = 5;

    [ObservableProperty]
    private ISeries[] _series = [];

    [ObservableProperty]
    private Axis[] _xAxes = [];

    [ObservableProperty]
    private Axis[] _yAxes = [];



    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _brownianService = new BrownianService();
        InitializeAxes();
        // Generate an initial path when the application starts
        StartSimulationCommand.Execute(null);
    }

    private void InitializeAxes()
    {
        var axisTextPaint = new SolidColorPaint(new SKColor(240, 240, 240));
        var axisSeparatorPaint = new SolidColorPaint(new SKColor(80, 80, 80)) { StrokeThickness = 1 };

        XAxes =
        [
            new Axis
            {
                Name = "Day",
                NamePaint = axisTextPaint,
                LabelsPaint = axisTextPaint,
                SeparatorsPaint = axisSeparatorPaint
            }
        ];

        YAxes =
        [
            new Axis
            {
                Name = "Price",
                NamePaint = axisTextPaint,
                LabelsPaint = axisTextPaint,
                SeparatorsPaint = axisSeparatorPaint,
                Labeler = Labelers.SixRepresentativeDigits
            }
        ];
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    private async Task StartSimulationAsync()
    {
        Debug.WriteLine($"Starting {NumberOfSimulations} simulations with {NumDays} days.");

        var dataModel = new BrownianDataModel
        {
            InitialPrice = InitialPrice,
            Sigma = Sigma,
            Mean = Mean,
            NumDays = NumDays
        };

        // To ensure thread safety and different random sequences for parallel tasks,
        // we create a new Random instance for each task, seeded from our main Random instance.
        var simulationTasks = Enumerable.Range(0, NumberOfSimulations)
            .Select(_ => Task.Run(() => _brownianService.GenerateSimulation(dataModel, new Random(_random.Next()))))
            .ToList();

        var results = await Task.WhenAll(simulationTasks);

        var seriesList = new List<ISeries>();
        int seriesIndex = 0;
        foreach (var prices in results)
        {
            var chartPoints = prices.Select((price, index) => new LiveChartsCore.Defaults.ObservablePoint(index, price));
            var color = s_palette[seriesIndex % s_palette.Length];

            seriesList.Add(new LineSeries<LiveChartsCore.Defaults.ObservablePoint>
            {
                Name = $"Path {seriesIndex + 1}",
                Values = chartPoints,
                Fill = null,
                GeometrySize = 0, // A geometry will be drawn when a point is hovered
                GeometryFill = new SolidColorPaint(color),
                GeometryStroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 1.5f },
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(color) { StrokeThickness = 2f },
                TooltipLabelFormatter = point => $"Day {point.SecondaryValue:N0}, Price: {point.PrimaryValue:C2}"
            });
            seriesIndex++;
        }

        Series = seriesList.ToArray();
    }

    [RelayCommand]
    private void NavigateToAbout() => _navigationService.NavigateToAboutView();
}