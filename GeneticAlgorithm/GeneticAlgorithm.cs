using System;

namespace GeneticAlgorithm
{
    internal class GeneticAlgorithm : IGeneticAlgorithm
    {
        private long _generationCount;
        private Generation _currentGeneration;
        public int PopulationSize { get; }
        public int NumberOfGenes { get; }
        public int LengthOfGene { get; }
        public double MutationRate { get; }
        public double EliteRate { get; }
        public int NumberOfTrials { get; }
        public FitnessEventHandler FitnessCalculation { get; }
        private int? _seed;

        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            this.PopulationSize = populationSize;
            this.NumberOfGenes = numberOfGenes;
            this.LengthOfGene = lengthOfGene;
            this.MutationRate = mutationRate;
            this.EliteRate = eliteRate;
            this.NumberOfTrials = numberOfTrials;
            this.FitnessCalculation = fitnessCalculation;
            this._seed = seed;
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

        public IGeneration CurrentGeneration { 
            get;
            set {
                // validate value type
                if (typeof(value).IsInstanceOfType(Generation) {
                    _currentGeneration = value;
                }
            }
        }
        public IGeneration GenerateGeneration()
        {
            // first generation is generated here
            if (GenerationCount == 0) {
                Generation generation = new Generation(this, this.FitnessCalculation, _seed);
                // loop generation chromosome array and create random chromosome at each index
                for (int i = 0; i < PopulationSize; i++) {
                    // new random chromosome
                    generation[i] = new Chromosome(NumberOfGenes, LengthOfGene, _seed);
                }
                CurrentGeneration = generation;
                _generationCount++;
            }

            // subsequent generations are generated here
            else {

            }

        }
    }
}