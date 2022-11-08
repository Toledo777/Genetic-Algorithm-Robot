using System;
using GeneticAlgorithm;
using System.Collections.Generic;
/**
 QUESTION FOR TEACHER: Due to walls count in the grid size??
 When do we write to a file, what is the number of moves? We think that its 200 or its how many moves it took to pick up all cans.

*/
namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        public int NumberOfActions { get; }

        public int NumberOfTestGrids { get; }

        public int GridSize { get; }

        public int NumberOfGenerations { get; }

        public double MutationRate { get; }

        public double EliteRate { get; }

        private int? seed;

        private Random _random;

        private GeneticAlgorithm.IGeneticAlgorithm _geneticAlgorithm;


        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null)
        {
            this.NumberOfGenerations = numberOfGenerations;
            this.seed = seed;
            if (seed != null)
            {
                _random = new Random((int)seed);
            }
            else
            {
                _random = new Random();
            }

        }

        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, double eliteRate, double mutationRate, int? seed = null)
        : this(numberOfGenerations, populationSize, numberOfTrials, seed)
        {
            this.EliteRate = eliteRate;
            this.MutationRate = mutationRate;
            this.MutationRate = mutationRate;
            this.GridSize = 10;
            // this._geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfTrials, MutationRate, EliteRate, ComputeFitness, seed);


        }

        // public delegate double FitnessEventHandler(IChromosome chromosome, IGeneration generation);

        public double ComputeFitness(IChromosome chromosome, IGeneration generation)
        {
            return 0.0;
        }



        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new NotImplementedException();
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            ContentsOfGrid[,] grid = new ContentsOfGrid[GridSize, GridSize];
            int numberOfCans = (GridSize * GridSize) / 2;

            List<int> randomNumbers = new List<int>();
            while (randomNumbers.Count < grid.Length)
            {
                int randomNumber = _random.Next(0, grid.Length);
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers.Add(randomNumber);
                }
            }

            int index = 0;
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    int legthOfGrid = GridSize-i * GridSize-j;
                    if (randomNumbers.Contains(legthOfGrid))
                    {

                    }
                    else
                    {
                        pos = ContentsOfGrid.Empty;
                    }
                }
            }


        }

            return null;
        }
}
}
