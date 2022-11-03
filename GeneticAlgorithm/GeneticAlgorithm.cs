using System;
using System.Collections.Generic;
using System.Linq;

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

        private int _eliteChromosomePopulationSize;

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
            this._eliteChromosomePopulationSize = (int)(this.PopulationSize * (this.EliteRate / 100));
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
            else // Reproduce Generation from CurrentGeneration 
            {
                this.CurrentGeneration = ReproduceNextGeneration();
                this.GenerationCount++;
                return this.CurrentGeneration;
            }
        }

        /// <summary>
        /// This method creates a new generation based of Elite Chromosomes 
        /// of the current generation.
        /// </summary>
        /// <returns> A new reproduced generation of chromosomes </returns>
        private IGeneration ReproduceNextGeneration()
        {
            IChromosome[] newGenerationChromosome = new IChromosome[PopulationSize];
        
            // Get All Elite Chromsomes
            int eliteChromosomePopulationSize = (int)(this.PopulationSize * (this.EliteRate / 100.00));
            IChromosome[] eliteChromsome = new Chromosome[this._eliteChromosomePopulationSize];

            eliteChromsome = this.CurrentGeneration.ChromosomeArr.OrderByDescending(x => x.Fitness).Take(_eliteChromosomePopulationSize).ToArray();

            // Copy Elite Chromosomes to new Generation
            for (int i = 0; i < eliteChromsome.Length; i++)
            {
                newGenerationChromosome[i] = eliteChromsome[i];
            }

            // Create the rest of the chromosomes
            // index is increased by 2 because we are adding 2 chromosomes at a time.
            for (int z = eliteChromsome.Length; z < this.PopulationSize; z += 2)
            {
                // Select the parents
                IChromosome[] children = GetReproducedChildren(eliteChromsome);

                newGenerationChromosome[z] = children[0];
                // Check if there is space for another child
                if (z + 1 < this.PopulationSize)
                {
                    newGenerationChromosome[z + 1] = children[1];
                }
            }

            return new Generation(newGenerationChromosome, this, this.FitnessCalculation, this._seed);
        }

        /// <summary>
        /// This method selects two parents from the elite chromosomes 
        /// and returns their reproduced children.
        /// </summary>
        /// <returns> Two children Chromosome created by elite Chromosomes. </returns>
        private IChromosome[] GetReproducedChildren(IChromosome[] eliteChromsome)
        {
            IChromosome parentA = eliteChromsome[this._random.Next(eliteChromsome.Length)];
            IChromosome parentB = eliteChromsome[this._random.Next(eliteChromsome.Length)];
            // Checks that two Parents are not the same
            while (true)
            {
                if (parentB.CompareTo(parentA) == 0)
                {
                    parentB = eliteChromsome[this._random.Next(eliteChromsome.Length)];
                    break;
                }
            }
            // Reproduce children
            IChromosome[] children = parentA.Reproduce(parentB, this.MutationRate);
            return children;
        }
    }
}

