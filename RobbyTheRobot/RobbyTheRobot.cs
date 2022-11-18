using System;
using GeneticAlgorithm;
using System.Collections.Generic;
using System.IO;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        public event FileWritten FileWrittenEvent;

        public int NumberOfActions { get; }

        public int NumberOfTestGrids { get; }

        public int GridSize { get; }

        public int NumberOfGenerations { get; }

        public double MutationRate { get; }

        public double EliteRate { get; }

        private int? seed;

        private Random _random;

        private GeneticAlgorithm.IGeneticAlgorithm _geneticAlgorithm;

        private event FitnessEventHandler _fitnessCalculation;

        private int _fileCount = 0;


        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfGenes, int lengthOfGene, int numberOfTrials, double mutationRate, double eliteRate,
         int? seed = null, int numberOfActions = 200, int numberOfTestGrids = 10, int gridSize = 10)
        {
            this._fitnessCalculation += ComputeFitness;
            this.FileWrittenEvent += (string file_path, long generation) => { Console.WriteLine($"Generation {generation}s' top chromosomes' fitness,number of moves, and genes was written to the file - {file_path}"); };
            this.NumberOfGenerations = numberOfGenerations;
            this.NumberOfTestGrids = numberOfTrials;
            this.seed = seed;
            this.NumberOfActions = numberOfActions;
            this.NumberOfTestGrids = numberOfTestGrids;
            this.GridSize = gridSize;
            this.MutationRate = mutationRate;
            this.EliteRate = eliteRate;
            this._geneticAlgorithm = GeneticLib.CreateGeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, _fitnessCalculation, seed);
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
        private double ComputeFitness(IChromosome chromosome, IGeneration generation)
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
            this._geneticAlgorithm.GenerateGeneration();
            // Write the following generations to file.
            if (this._geneticAlgorithm.GenerationCount == 1 || this._geneticAlgorithm.GenerationCount == 19 || this._geneticAlgorithm.GenerationCount == 99
             || this._geneticAlgorithm.GenerationCount == 199 || this._geneticAlgorithm.GenerationCount == 499 || this._geneticAlgorithm.GenerationCount == 999)
            {
                // Create file
                long genCountIndexOne = this._geneticAlgorithm.GenerationCount;
                // Incerment by one to get the Generation Count based on 1 index
                if (this._geneticAlgorithm.GenerationCount != 1) { genCountIndexOne = this._geneticAlgorithm.GenerationCount + 1; }
                // Create file
                var file_name = $"Generation{++this._fileCount}.txt";
                var file_path = System.IO.Path.Combine(folderPath, file_name);
                // Create string from top chromosome
                string line = $"{genCountIndexOne},{this._geneticAlgorithm.CurrentGeneration[0].Fitness},{this.NumberOfActions},{this._geneticAlgorithm.CurrentGeneration[0].ToString()}";
                // Append to file
                File.WriteAllLinesAsync(file_path, new List<string> { line });
                // Notify user
                this.FileWrittenEvent(file_path, genCountIndexOne);
            }
        }


        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            ContentsOfGrid[,] grid = new ContentsOfGrid[GridSize, GridSize];
            int numberOfCans = (GridSize * GridSize) / 2;
            List<int> randomNumbers = new List<int>();
            // generates random unique ints until we have enough to fill half of the grid

            while (randomNumbers.Count < grid.Length / 2)
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
                grid[coords[0], coords[1]] = ContentsOfGrid.Can;
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

