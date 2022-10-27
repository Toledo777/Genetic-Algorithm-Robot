namespace GeneticAlgorithm
{
    public class Generation : IGenerationDetails
    {

        private IChromosome[] _chromosomeArr; //change later to chromosome??
        private IGeneticAlgorithm _geneticAlgorithm;
        private int _seed;
        private double AverageFitness => throw new System.NotImplementedException();

        private double MaxFitness => throw new System.NotImplementedException();

        

        // regular constructor
        public Generation(IGeneticAlgorithm geneticAlgorithm ,FitnessEventHandler fitnessHandler, int seed) {
            _seed = seed;
            _geneticAlgorithm = geneticAlgorithm;
        }

        // copy constructor
        public Generation() {
            
        }

        private long NumberOfChromosomes() {
            return _chromosomeArr.Length;
        }

        public IChromosome this[int index] {
            get { return _chromosomeArr[index]; }
            set { _chromosomeArr[index] = value; }
        }

        public IChromosome SelectParent() {
            
        }

        public void EvaluateFitnessOfPopulation() {

        }


    }
}