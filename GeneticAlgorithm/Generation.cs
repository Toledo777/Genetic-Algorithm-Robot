namespace GeneticAlgorithm
{
    public class Generation : IGenerationDetails
    {

        private IChromosome[] _chromosomeArr; //change later to chromosome??
        private IGeneticAlgorithm _geneticAlgorithm;
        private int _seed;
        private double AverageFitness => throw new System.NotImplementedException();

        private FitnessEventHandler _fitnessHandler;
        

        // regular constructor
        public Generation(IGeneticAlgorithm geneticAlgorithm ,FitnessEventHandler fitnessHandler, int seed) {
            _seed = seed;
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessHandler  =fitnessHandler;
        }

        // copy constructor
        public Generation() {
            int seed = _seed;
            // TO-DO add parameters
            IGeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
        }

        private long NumberOfChromosomes() {
            return _chromosomeArr.Length;
        }

        private double MaxFitness() {
            double max = 0;

            // loop chromosomes to get max
            for (int i = 0; i < _chromosomeArr.Length; i++) {
                if (_chromosomeArr[i].Fitness > max) {
                    max = _chromosomeArr[i].Fitness;
                }
            }

            return max;
        }

        // Retrieves the IChromosome from the generation
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