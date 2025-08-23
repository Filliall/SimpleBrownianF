using SimpleBrownianF.Models;
using System;

namespace SimpleBrownianF.Services
{
    public interface IBrownianService
    {
        double[] GenerateSimulation(BrownianDataModel data, Random random);
    }
}