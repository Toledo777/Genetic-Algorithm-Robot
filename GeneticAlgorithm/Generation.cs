namespace GeneticAlgorithm
{
    public class Generation : IGenerationDetails
    {

        private IChromosome[] _chromosomeArr; //change later to chromosome??
        private IGeneticAlgorithm _geneticAlgorithm;
        private int _seed;
        private FitnessEventHandler _fitnessHandler;
        private long _numberOfChromosomes;
        private double _maxFitness;
        private double _averageFitness;


        // regular constructor
        public Generation(IGeneticAlgorithm geneticAlgorithm ,FitnessEventHandler fitnessHandler, int seed) {
            _seed = seed;
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessHandler  =fitnessHandler;
            _averageFitness = calculateAverageFitness();
            _maxFitness = calculateMaxFitness();
        }

        // copy constructor
        public Generation() {
            int seed = _seed;
            // TO-DO add parameters
            IGeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
        }

        // property
        public long NumberOfChromosomes {
            get {return _chromosomeArr.Length; }
        }

        // property
        public double AverageFitness {
            get { return _averageFitness;}
            set { _averageFitness = value;}
        }
        
        // property
        public double MaxFitness {
            get { return _maxFitness;}
            set {_maxFitness = value;}
        }

        // helper function
        private double calculateAverageFitness() {
            // sum of fitness of all chromosomes
            double sum = 0;
            for (int i = 0; i < _chromosomeArr.Length; i++) {
                sum+= _chromosomeArr[i].Fitness;
            }

            // return and calculate average
            return sum / _chromosomeArr.Length;
        }

        // helper function
        private double calculateMaxFitness() {
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