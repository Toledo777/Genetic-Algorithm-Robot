using System;
using System.Collections.Generic;
namespace GeneticAlgorithm
{
    internal class GeneticAlgorithm : IGeneticAlgorithm
    {
        private long _generationCount;

        public int PopulationSize { get; }

        public int NumberOfGenes { get; }

        public int LengthOfGene { get; }

        public double MutationRate { get; }

        public double EliteRate { get; }

        public int NumberOfTrials { get; }

        public FitnessEventHandler FitnessCalculation { get; }

        private int? _seed;

        private Random _random;

        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            this.PopulationSize = populationSize;
            this.NumberOfGenes = numberOfGenes;
            this.LengthOfGene = lengthOfGene;
            this.MutationRate = mutationRate;
            this.EliteRate = eliteRate; // Given in %/100 tranform to decimal/1.0
            this.NumberOfTrials = numberOfTrials;
            this.FitnessCalculation = fitnessCalculation;
            this._seed = seed;
            if (seed != null)
            {
                _random = new Random((int)seed);
            }
            else
            {
                _random = new Random();
            }
        }


        public long GenerationCount
        {
            get { return this._generationCount; }
            set
            {
                // Ensures that the new value is the newer genration
                if (this._generationCount < value)
                {
                    throw new ArgumentException($"You can't set an older generation Number: {this._generationCount}. to a newer one: {value}");
                }
                this._generationCount = value;
            }
        }

        public IGeneration CurrentGeneration { get; set; }


        public IGeneration GenerateGeneration()
        {
            // Inital Generation - First Generation
            if (this.GenerationCount == 0)
            {
                Generation generation = new Generation(this, this.FitnessCalculation, _seed);

                for (int i = 0; i < this.PopulationSize; i++)
                {
                    generation[i] = new Chromosome(this.NumberOfGenes, this.LengthOfGene, this._seed);
                }
                this.GenerationCount++;

                this.CurrentGeneration = generation;
                return generation;

            }
            // Reproduce Generation from CurrentGeneration
            else
            {
                this.CurrentGeneration = ReproduceNextGeneration();
                return this.CurrentGeneration;
            }

        }

        /// <summary>
        /// This method creates a new generation based Elite Chromosomes 
        /// of the current generation.
        /// </summary>
        /// <returns> A new generation of chromosome with IGeneration </returns>
        private IGeneration ReproduceNextGeneration()
        {
            IChromosome[] newGenerationChromosome = new IChromosome[PopulationSize];
            List<IChromosome> eliteChromsome = new List<IChromosome>();
            // Define fitness of the elite chromosomes
            double eliteChromosomeFitnessLowerLimit = this.CurrentGeneration.MaxFitness - 30;
            // Select the elite chromosomes
            for (int i = 0; i < this.PopulationSize; i++)
            {
                if (this.CurrentGeneration[i].Fitness >= eliteChromosomeFitnessLowerLimit)
                {
                    // Check elite rate to add to list
                    if (this._random.NextDouble() <= (this.EliteRate / 100.00))
                    {
                        eliteChromsome.Add(this.CurrentGeneration[i]);
                    }
                }
            }

            // Reproduce the elite chromosomes
            // i is increased by 2 because we are adding 2 chromosomes at a time.
            for (int i = 0; i < eliteChromsome.Count; i +=2)
            {
                newGenerationChromosome[i] = eliteChromsome[i];
            }

            // Create the rest of the chromosomes
            for (int i = eliteChromsome.Count; i < this.PopulationSize; i++)
            {
                // Select the parents
                IChromosome[] children = GetReproducedChildren(eliteChromsome);

                newGenerationChromosome[i] = children[0];
                // Check if there is space for another child
                if (i++ < this.PopulationSize)
                {
                    newGenerationChromosome[i++] = children[1];
                }
            }

            return new Generation(newGenerationChromosome);
        }

        /// <summary>
        /// This method selects two parents from the elite chromosomes 
        /// and returns their reproduced children.
        /// </summary>
        /// <returns> Two children Chromosome created by elite Chromosomes. </returns>
        private IChromosome[] GetReproducedChildren(List<IChromosome> eliteChromsome)
        {
            IChromosome parentA = eliteChromsome[this._random.Next(eliteChromsome.Count)];
            IChromosome parentB = eliteChromsome[this._random.Next(eliteChromsome.Count)];
            while (true)
            {
                if (parentB.CompareTo(parentA) == 0)
                {
                    parentB = eliteChromsome[this._random.Next(eliteChromsome.Count)];
                    break;
                }
            }
            // Reproduce children
            IChromosome[] children = parentA.Reproduce(parentB, this.MutationRate);
            return children;
        }
    }
}

