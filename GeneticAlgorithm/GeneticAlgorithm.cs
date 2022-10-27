using System;

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
        public IGeneration CurrentGeneration { get; }
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
      
        }

        public IGeneration GenerateGeneration()
        {
            throw new System.NotImplementedException();
        }
    }
}