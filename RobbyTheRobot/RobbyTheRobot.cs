using System;
using GeneticAlgorithm;
using System.Collections.Generic;
using System.IO;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        public event FileWritten FileWrittenEvent;

        public int NumberOfActions { get; } // 200

        public int NumberOfTestGrids { get; }

        public int GridSize { get; }

        public int NumberOfGenerations { get; }

        public double MutationRate { get; } //5

        public double EliteRate { get; }//10

        private int? seed;

        private Random _random;

        private GeneticAlgorithm.IGeneticAlgorithm _geneticAlgorithm;

        private event FitnessEventHandler _fitnessCalculation;


        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfGenes, int lengthOfGene, int numberOfTrials, int? seed = null,
         int numberOfActions = 200, int numberOfTestGrids = 10, int gridSize = 10, double mutationRate = 0.05, double eliteRate = 0.1)
        {
            this._fitnessCalculation += ComputeFitness;
            this.FileWrittenEvent += () => { Console.WriteLine($"{this._geneticAlgorithm.GenerationCount} top chromosomes' fitness,number of moves, and genes was written to the file."); };
            this.NumberOfGenerations = numberOfGenerations;
            this.NumberOfTestGrids = numberOfTrials;
            this.seed = seed;
            this.NumberOfActions = numberOfActions;
            this.NumberOfTestGrids = numberOfTestGrids;
            this.GridSize = gridSize;
            this.MutationRate = mutationRate;
            this.EliteRate = eliteRate;
            this._geneticAlgorithm = GeneticLib.CreateGeneticAlgorithm(populationSize, numberOfGenes,lengthOfGene, mutationRate, eliteRate, numberOfTrials, _fitnessCalculation, seed);
            if (seed != null)
            {
                _random = new Random((int)seed);
            }
            else
            {
                _random = new Random();
            }

        }

        ///<summary>
        /// This method is called by the GeneticAlgorithm to compute the fitness of a chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome to compute the fitness of.</param>
        /// <param name="generation">The generation the chromosome is in. - this is only to acces parameters if need be </param>
        /// <returns>The fitness of the chromosome.</returns>
        public double ComputeFitness(IChromosome chromosome, IGeneration generation)
        {
            double score = 0;
            int x = _random.Next(0, GridSize);
            int y = _random.Next(0, GridSize);

            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            for (int i = 0; i <= this.NumberOfActions; i++)
            {
                double res = RobbyHelper.ScoreForAllele(chromosome.Genes, grid, this._random, ref x, ref y);
                score += res;
            }

            return score;
        }



        public void GeneratePossibleSolutions(string folderPath)
        {
            IChromosome topChromosome = this._geneticAlgorithm.CurrentGeneration[0];
            File.WriteAllTextAsync(folderPath, $"{topChromosome.Fitness},{this.NumberOfActions},{topChromosome.ToString()}");
            this.FileWrittenEvent();
        }


        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            ContentsOfGrid[,] grid = new ContentsOfGrid[GridSize, GridSize];
            int numberOfCans = (GridSize * GridSize) / 2;
            List<int> randomNumbers = new List<int>();
            // generates random unique ints until we have enough to fill half of the grid

            while (randomNumbers.Count <= grid.Length / 2)
            {
                int randomNumber = _random.Next(0, grid.Length);
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers.Add(randomNumber);
                }
            }

            // Fills half the grid with cans
            for (int i = 0; i < randomNumbers.Count; i++)
            {
                int[] coords = ConvertIntToCoords(randomNumbers[i], GridSize);
                grid[coords[0], coords[1]] = ContentsOfGrid.Can; // may need to be -1
            }

            return grid;
        }


        /// <summary>
        /// Converts an integer position to coordinates [y, x] representing the same position.
        /// </summary>
        /// <param name="pos">Integer representing position on the grid, range 0 to gridSize^2-1</param>
        /// <param name="gridSize">Height and width of the grid</param>
        /// <returns>int[] holding coordinates corresponding to the 2 ints necessary to reach an element of ContentOfGrid[,]</returns>
        private int[] ConvertIntToCoords(int pos, int gridSize)
        {
            int[] coords = new int[2];
            // Calculate height (y coord) 
            coords[0] = pos / gridSize;
            // Calcuates length (x position)
            coords[1] = pos % gridSize;

            return coords;
        }

    }

}

