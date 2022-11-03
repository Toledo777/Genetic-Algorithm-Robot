 using System;

namespace GeneticAlgorithm
{
   
    public class Generation : IGenerationDetails
    {
        private Chromosome[] _chromosomeArr;
        private IGeneticAlgorithm _geneticAlgorithm;
        private int? _seed;
        private FitnessEventHandler _fitnessHandler;
        private long _numberOfChromosomes;
        private const int _parentRange = 10;

        // constructor, only called the first time
        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessHandler, int? seed = null) 
        {
            this._seed = seed;
            this._geneticAlgorithm = geneticAlgorithm;
            this. _fitnessHandler  = fitnessHandler;
            this.AverageFitness = calculateAverageFitness();
            this.MaxFitness = calculateMaxFitness();
        }

        // copy constructor
        public Generation(IChromosome[] chromArr, IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessHandler, int? seed = null) : this(geneticAlgorithm, fitnessHandler, seed)
        {
            this._chromosomeArr = new Chromosome[chromArr.Length];

            for (int i = 0; i < chromArr.Length; i++) {
                // calls copy constructor of chromosome and sets the copy chromosome;
                _chromosomeArr[i] = new Chromosome(chromArr[i] as Chromosome);
            }

            this.AverageFitness = calculateAverageFitness();
            this.MaxFitness = calculateMaxFitness();
        }

        /// <summary>
        /// Added internal property that returns the chromosome array
        /// </summary>
        /// <returns> Chromosome[] </returns>
        internal Chromosome[] ChromosomeArr
        {
            get {return this._chromosomeArr;}
        }

        public long NumberOfChromosomes 
        {
            get {return _chromosomeArr.Length;}
        }

        public double AverageFitness 
        {
            get
            {
                AverageFitness = calculateAverageFitness();
                return AverageFitness;
            }
            set {
                if (value > 0) {
                    AverageFitness = value;
                }
            }
        }
        
        public double MaxFitness 
        {
            get 
            {
                MaxFitness = calculateMaxFitness();
                return MaxFitness;
            }
            set {
                if (value > 0) {
                    MaxFitness = value;
                }
            }
        }

        /// <summary>
        /// Sums fitness of all chromosome and computes average
        /// </summary>
        /// <returns> double of average fitness of all chromosome </returns>
        private double calculateAverageFitness() 
        {
            // sum of fitness of all chromosomes
            double sum = 0;
            for (int i = 0; i < _chromosomeArr.Length; i++) 
            {
                sum+= _chromosomeArr[i].Fitness;
            }

            // return and calculate average
            return sum / _chromosomeArr.Length;
        }

        /// <summary>
        /// Computes the highest fitness in the chromosome array and returns it.`1
        /// </summary>
        /// <returns> Max fitness </returns>
        private double calculateMaxFitness() 
        {
            double max = 0;

            // loop chromosomes to get max
            for (int i = 0; i < _chromosomeArr.Length; i++) 
            {
                if (_chromosomeArr[i].Fitness > max) {
                    max = _chromosomeArr[i].Fitness;
                }
            }
            return max;
        }

        // Retrieves the IChromosome from the generation
        public IChromosome this[int index] 
        {
            get { return _chromosomeArr[index]; }
            set { _chromosomeArr[index] = value as Chromosome; }
        }

        // select random parents from top range
        public IChromosome SelectParent() 
        {
            // sort array using CompareTo method of chromosome
            Array.Sort(_chromosomeArr);
            Random rng;
            if (_seed != null) 
                rng = new Random((int)_seed);
            else
                rng = new Random();
            
            // get random index from 0 to _parentRange
            int randParentIndex = rng.Next(0, _parentRange);
            return _chromosomeArr[randParentIndex];
        }

        public void EvaluateFitnessOfPopulation() {
            // evaluate fitness of each chromosome in array
            for (int i = 0; i < _geneticAlgorithm.PopulationSize; i++) 
            {
                if (_geneticAlgorithm.NumberOfTrials > 0)
                {
                    double fitnessSum = 0;
                    for (int j = 0; j < _geneticAlgorithm.NumberOfTrials; j++) 
                    {
                        fitnessSum += _fitnessHandler(_chromosomeArr[i], this);
                    }
                    // assign fitness to be average of all trials for that chromosome
                    _chromosomeArr[i].Fitness = fitnessSum / _geneticAlgorithm.NumberOfTrials;
                }

                // when number of trials is 0 or negative
                else {
                    throw new InvalidOperationException("Cannot compute fitness without at least 1 trial");
                }     
            }
        }
    }
}