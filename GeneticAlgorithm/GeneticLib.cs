namespace GeneticAlgorithm
{
    public static class GeneticLib
    {
        public static IGeneticAlgorithm CreateGeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            return new GeneticAlgorithm(populationSize, numberOfGenes /*Array of Chromsome*/, lengthOfGene /* Number of Actions*/, mutationRate, eliteRate, numberOfTrials, fitnessCalculation, seed);
        }
    }
}
