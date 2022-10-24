namespace GeneticAlgorithm
{
    public class GenerationDetails : IGenerationDetails
    {


        private IChromosome[] _chromosomeArr; //change later to chromosome??
        private IGeneticAlgorithm _geneticAlgorithm;

        private double AverageFitness => throw new System.NotImplementedException();

        private double MaxFitness => throw new System.NotImplementedException();

        private long NumberOfChromosomes => throw new System.NotImplementedException();

        // regular constructor
        public GenerationDetails(IGeneticAlgorithm geneticAlgorithm ,FitnessEventHandler fitnessHandler, int seed) {
            _geneticAlgorithm = geneticAlgorithm;
        }

        // copy constructor
        public GenerationDetails() {
            
        }

        public IChromosome this[int index] {

        }

        public IChromosome SelectParent() {
            
        }

        public void EvaluateFitnessOfPopulation() {

        }


    }
}