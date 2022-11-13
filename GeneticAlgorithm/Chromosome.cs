using System.Diagnostics.CodeAnalysis;
using System;

namespace GeneticAlgorithm
{
    internal class Chromosome : IChromosome
    {
        private Random _rng;
        private int? _seed;

        public double Fitness
        {
            get;
            internal set;
        }

        public int[] Genes
        {
            get;
            private set;
        }

        public long Length
        {
            get;
        }

        public int this[int index]
        {
            get { return this.Genes[index]; }
            set { this.Genes[index] = value; }
        }
        public int NumGenes { get; }
        public int? Seed => _seed;

        //constructor 1
        public Chromosome(int numGenes, long geneLength, int? seed = null)
        {
            this._seed = seed;
            this.NumGenes = numGenes;
            this.Length = geneLength;
            this.Genes = new int[numGenes];
            this.Fitness = 0;
            if (this._seed != null)
            {
                this._rng = new Random((int)seed);
            }
            else
            {
                this._rng = new Random();
            }
            for (int i = 0; i < this.NumGenes; i++)
            {
                // {
                Genes[i] = _rng.Next(0, (int)Length); // random number between 1 and 7.
                // }
            }

        }
        //constructor 2:
        // when creating a child, the genes are already defined. Thus this overloaded constructor allows for the creation of children with predefined genes.
        public Chromosome(long geneLength, int[] genes, int? seed = null) : this(genes.Length, geneLength, seed)
        {
            this.Genes = genes;
        }


        //constructor 3
        public Chromosome(Chromosome chromosome) : this(chromosome.Length, chromosome.Genes, chromosome.Seed)
        {
            int[] genes = new int[NumGenes];
            for (int i = 0; i < NumGenes; i++)
            {
                genes[i] = chromosome.Genes[i];
            }
            this.Genes = genes;
        }

        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            // int numberOfGenesInSpouse = ((Chromosome)spouse).NumGenes;
            if (this.NumGenes != ((Chromosome)spouse).NumGenes)
            {
                throw new ArgumentException("Parents must have same number of genes");
            }
            if (this.Length != ((Chromosome)spouse).Length)
            {
                throw new ArgumentException("Parents' Gene length must be the same");
            }

            IChromosome[] children = new Chromosome[2];
            children[0] = this.GenerateChild((Chromosome)spouse).MutateChromosome(mutationProb);
            children[1] = ((Chromosome)spouse).GenerateChild(this).MutateChromosome(mutationProb);
            return children;
        }

        /// <summary>
        ///Creates child with spouse chromosome
        /// </summary>
        /// <param name="spouse">The chromosome whose genes will be combind with the current chromosome to generate a child chromosome </param>
        /// <returns> A "child" chromosome whose genes is a combination of its parents</returns>
        Chromosome GenerateChild(Chromosome spouse)
        {
            //we want to create a start and end index, these positions will be the ones whose genes are swapped with the second parent.
            int startIndP2 = this._rng.Next(0, spouse.NumGenes - 1);
            int endIndP2 = this._rng.Next(startIndP2, this.NumGenes);
            //genes to swap
            int[] genes = new int[this.NumGenes];
            //deep copy
            for (int i = 0; i < this.NumGenes; i++)
            {
                genes[i] = this[i];
            }
            for (int i = startIndP2; i < endIndP2; i++)
            {
                genes[i] = spouse[i];
            }
            Chromosome child = new Chromosome(this.Length, genes, this.Seed); //need to check that NumGenes and Seed get the right things.
            return child;
        }

        /// <summary>
        ///Mutates at the rate of the mutationProb (eg: 0.1 = 10%, 1+ = 100%)
        /// </summary>
        /// <param name="mutationProb">A double representing the mutation probability</param>
        /// <returns>The chromosome after having been mutated. </returns>
        IChromosome MutateChromosome(double mutationProb)
        {
            for (int i = 0; i < this.NumGenes; i++)
            {
                if (this._rng.NextDouble() <= mutationProb)
                {
                    this[i] = _rng.Next(0, (int)this.Length);
                }
            }
            return this;
        }

        /// <summary>
        /// Compares two IChromosomes based on their fitness.
        /// </summary>
        /// <param name="other">The Chromosome to compare the current one with</param>
        /// <returns> 0 if the IChromosomes' fitness are equal, 1 if the current one's is higher, -1 if it is lower. </returns>
        public int CompareTo(IChromosome other)
        {
            if (this.Fitness == other.Fitness)
            {
                return 0;
            }
            else if (this.Fitness > other.Fitness)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public override string ToString()
        {
            string s = "Genes in Chromosome: ";
            for (int i = 0; i < this.NumGenes; i++)
            {
                s += this[i] + " ";
            }
            return s;

        }

    }
}