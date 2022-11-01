namespace GeneticAlgorithm
{
    using System;
    public class Generation : IGenerationDetails
    {

        private IChromosome[] _chromosomeArr; //change later to chromosome??
        private IGeneticAlgorithm _geneticAlgorithm;
        private int? _seed;
        private static FitnessEventHandler _fitnessHandler;
        private long _numberOfChromosomes;
        private double _maxFitness;
        private double _averageFitness;
        private const int _parentRange = 10;

        // constructor, only called the first time
        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessHandler, int? seed = null) {
            _seed = seed;
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessHandler  = fitnessHandler;
            AverageFitness = calculateAverageFitness();
            MaxFitness = calculateMaxFitness();
        }

        // copy constructor
        public Generation(IChromosome[] chromArr) {
            _chromosomeArr = new IChromosome[chromArr.Length];

            for (int i = 0; i < chromArr.Length; i++) {
                // calls copy constructor of chromosome and sets the copy chromosome;
                _chromosomeArr[i] = new Chromosome(chromArr[i]);
            }

            _averageFitness = calculateAverageFitness();
            _maxFitness = calculateMaxFitness();
        }

        // property
        public long NumberOfChromosomes {
            get {return _chromosomeArr.Length; }
        }

        // property
        public double AverageFitness {
            get;
            set;
        }
        
        // property
        public double MaxFitness {
            get;
            set;
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

        // select random parents from top range
        public IChromosome SelectParent() {
            // sort array using CompareTo method of chromosome
            Array.Sort(_chromosomeArr);
            Random rng;
            if (_seed != null) {
                rng = new Random((int)_seed);
            }
            R
            // get random index from 0 to _parentRange
            int randParentIndex = rng.Next(0, _parentRange);
            return _chromosomeArr[randParentIndex];
        }

        // unsure about how this function should be made
        public void EvaluateFitnessOfPopulation() {
            for (int i = 0; i < _chromosomeArr.Length; i++) {
                // call fitness handler for each chromosome
               _chromosomeArr[i].Fitness = _fitnessHandler(_chromosomeArr[i], this);
            }
        }


    }
}