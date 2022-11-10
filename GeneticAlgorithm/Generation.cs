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
        private Random _rng;

        // constructor, only called the first time
        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessHandler, int? seed = null)
        {
            this._seed = seed;
            this._geneticAlgorithm = geneticAlgorithm;
            this._fitnessHandler = fitnessHandler;
            this.AverageFitness = calculateAverageFitness();
            this.MaxFitness = calculateMaxFitness();

            if (_seed != null) 
                this._rng = new Random((int) _seed);
            else
                this._rng= new Random();
        }

        // copy constructor
        public Generation(IChromosome[] chromArr, IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessHandler, int? seed = null) : this(geneticAlgorithm, fitnessHandler, seed)
        {
            this._chromosomeArr = new Chromosome[chromArr.Length];

            for (int i = 0; i < chromArr.Length; i++)
            {
                // calls copy constructor of chromosome and sets the copy chromosome;
                this._chromosomeArr[i] = new Chromosome(chromArr[i] as Chromosome);
            }
        }

        /// <summary>
        /// internal property that returns the chromosome array
        /// </summary>
        /// <returns> Chromosome[] </returns>
        internal Chromosome[] ChromosomeArr
        {
            get { return this._chromosomeArr; }
        }

        public long NumberOfChromosomes
        {
            get { return this._geneticAlgorithm.PopulationSize; }
        }

        public double AverageFitness
        {
            get
            {
                AverageFitness = calculateAverageFitness();
                return AverageFitness;
            }
            set
            {
                AverageFitness = value;
            }
        }

        public double MaxFitness
        {
            get
            {
                MaxFitness = calculateMaxFitness();
                return MaxFitness;
            }
            set
            {
                MaxFitness = value;
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
            for (int i = 0; i < this._chromosomeArr.Length; i++)
            {
                sum += this._chromosomeArr[i].Fitness;
            }

            // return and calculate average
            return sum / this._chromosomeArr.Length;
        }

        /// <summary>
        /// Computes the highest fitness in the chromosome array and returns it
        /// </summary>
        /// <returns> Max fitness </returns>
        private double calculateMaxFitness()
        {
            // set max to fitness of first chromosome
            double max = _chromosomeArr[0].Fitness;

            // loop chromosomes to get max starting at index 1
            for (int i = 1; i < this._chromosomeArr.Length; i++)
            {
                if (this._chromosomeArr[i].Fitness > max)
                {
                    max = this._chromosomeArr[i].Fitness;
                }
            }
            return max;
        }

        // Retrieves the IChromosome from the generation
        public IChromosome this[int index]
        {
            get { return this._chromosomeArr[index]; }
            set { this._chromosomeArr[index] = value as Chromosome; }
        }

        // select random parents from top range
        public IChromosome SelectParent()
        {
            // sort array using CompareTo method of chromosome
            Array.Sort(_chromosomeArr);
            // Reverse array to get chromosomes in descending order
            Array.Reverse(_chromosomeArr);


            // get random index from 0 to _parentRange
            int randParentIndex = _rng.Next(0, _parentRange);
            return this._chromosomeArr[randParentIndex];
        }

        public void EvaluateFitnessOfPopulation()
        {
            // evaluate fitness of each chromosome in array
            for (int i = 0; i < _chromosomeArr.Length; i++)
            {
                if (this._geneticAlgorithm.NumberOfTrials > 0)
                {
                    double fitnessSum = 0;
                    for (int j = 0; j < _geneticAlgorithm.NumberOfTrials; j++)
                    {
                        fitnessSum += this._fitnessHandler(_chromosomeArr[i], this);
                    }

                    // assign fitness to be average of all trials for that chromosome
                    this._chromosomeArr[i].Fitness = fitnessSum / this._geneticAlgorithm.NumberOfTrials;
                }

                // when number of trials is 0 or negative
                else
                {
                    throw new InvalidOperationException("Cannot compute fitness without at least 1 trial");
                }
            }
        }
    }
}
