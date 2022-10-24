namespace GeneticAlgorithm
{
    public class GenerationDetails : IGenerationDetails
    {


        private Chromosome[] _chromosomeArr;

        // regular constructor
        public GenerationDetails(IGeneticAlgorithm geneticAlgorithm ,FitnessEventHandler fitnessHandler, int seed) {

        }

        // copy constructor
        public GenerationDetails() {

        }

        int PopulationSize { get; }
        int NumberOfGenes { get; }
        int LengthOfGene { get; }
        double MutationRate { get; }
        double EliteRate { get; }
    }
}