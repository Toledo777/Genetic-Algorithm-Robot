using System;
using System.Linq;

namespace GeneticAlgorithm
{
    internal class GeneticAlgorithm : IGeneticAlgorithm
    {

        private Random _random;

        private int _eliteChromosomePopulationSize;
        private long _generationCount;

        public int PopulationSize { get; }

        public int NumberOfGenes { get; }

        public int LengthOfGene { get; }

        public double MutationRate { get; }

        public double EliteRate { get; }

        public int NumberOfTrials { get; }

        public FitnessEventHandler FitnessCalculation { get; }

        public int? Seed { get; }


        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            this.GenerationCount = 0;
            this.PopulationSize = populationSize;
            this.NumberOfGenes = numberOfGenes;
            this.LengthOfGene = lengthOfGene;
            this.MutationRate = mutationRate;
            this.EliteRate = eliteRate; // Given in %/100 tranform to decimal/1.0
            this.NumberOfTrials = numberOfTrials;
            this.FitnessCalculation = fitnessCalculation;
            this.Seed = seed;
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
                if (this._generationCount > value)
                {
                    throw new ArgumentException($"You can't set an older generation Number: {this._generationCount}. to a newer one: {value}");
                }
                // Ensure that the new value is only one generation ahead
                if (value - this._generationCount > 1)
                {
                    throw new ArgumentException($"You can't set a generation Number: {value} that is more than one generation ahead of the current generation: {this._generationCount}");
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
                // Generation generation = new Generation(this, this.FitnessCalculation, this.Seed);
                IChromosome[] chromosome = new Chromosome[PopulationSize];
                for (int i = 0; i < this.PopulationSize; i++)
                {
                    chromosome[i] = new Chromosome(this.NumberOfGenes, this.LengthOfGene, this.Seed);
                }

                Generation generation = new Generation(chromosome, this, this.FitnessCalculation, Seed);
                generation.EvaluateFitnessOfPopulation();
                // Sort Chromsomes by Descending Fitness
                Array.Sort(generation.ChromosomeArr);
                Array.Reverse(generation.ChromosomeArr);

                this.GenerationCount++;
                this.CurrentGeneration = generation;

                return this.CurrentGeneration;

            }
            else // Reproduce Generation from CurrentGeneration 
            {
                Generation generation = ReproduceNextGeneration();
                // Sort Chromsomes by Descending Fitness
                Array.Sort(generation.ChromosomeArr);
                Array.Reverse(generation.ChromosomeArr);

                this.GenerationCount += 1;
                this.CurrentGeneration = generation;

                return this.CurrentGeneration;
            }
        }

        /// <summary>
        /// This method creates a new generation based of Elite Chromosomes 
        /// of the current generation.
        /// </summary>
        /// <returns> A new reproduced generation of chromosomes </returns>
        private Generation ReproduceNextGeneration()
        {
            IChromosome[] newGenerationChromosome = new IChromosome[PopulationSize];

            // Get All Elite Chromsomes
            int eliteChromosomePopulationSize = (int)(this.PopulationSize * (this.EliteRate / 100.00));
            IChromosome[] eliteChromsome = new Chromosome[this._eliteChromosomePopulationSize];

            eliteChromsome = (this.CurrentGeneration as Generation).ChromosomeArr.OrderByDescending(x => x.Fitness).Take(_eliteChromosomePopulationSize).ToArray();

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
                IChromosome[] children = GetReproducedChildren(); 

                newGenerationChromosome[z] = children[0];
                // Check if there is space for another child
                if (z + 1 < this.PopulationSize)
                {
                    newGenerationChromosome[z + 1] = children[1];
                }
            }

            Generation newGenerationReproduced = new Generation(newGenerationChromosome, this, this.FitnessCalculation, this.Seed);
            newGenerationReproduced.EvaluateFitnessOfPopulation();

            return newGenerationReproduced;
        }

        /// <summary>
        /// This method selects two parents from the current generation
        /// and returns their reproduced children.
        /// </summary>
        /// <returns> Two children Chromosome created by elite Chromosomes. </returns>
        private IChromosome[] GetReproducedChildren() //(IChromosome[] eliteChromsome)
        {
            IChromosome parentA = (this.CurrentGeneration as Generation).SelectParent(); // eliteChromosme[this._random.Next(eliteChromsome.Length)];
            IChromosome parentB = (this.CurrentGeneration as Generation).SelectParent();// eliteChromsome[this._random.Next(eliteChromsome.Length)];
            // Checks that two Parents are not the same
            while (true)
            {
                // Check if parentA and parentB are the same
                if (parentA == parentB)
                {
                    parentB = (this.CurrentGeneration as Generation).SelectParent();//eliteChromsome[this._random.Next(eliteChromsome.Length)];
                }
                else
                {
                    break;
                }
            }
            // Reproduce children
            IChromosome[] children = parentA.Reproduce(parentB, this.MutationRate);
            return children;
        }
    }
}

