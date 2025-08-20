using System;
using SimpleBrownianF.Models;

namespace SimpleBrownianF.Services
{
    public interface IBrownianService
    {
        double[] GenerateSimulation(BrownianDataModel data, Random random);
    }
}