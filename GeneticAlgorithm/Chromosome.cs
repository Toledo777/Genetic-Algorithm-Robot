using System.Diagnostics.CodeAnalysis;
using System;

namespace GeneticAlgorithm
{
    internal class Chromosome : IChromosome
    {
        private Random _rng;
        private int? _seed = null;

        public double Fitness {
            get;
            set;
        }

        public int[] Genes {
            get;
        }

        public long Length{
            get;
        }

        public int this[int index] {
            get{return this.Genes[index];}
            set {this.Genes[index] = value;}
            }
        public int NumGenes => Genes.Length;
        public int? Seed => _seed;
        public Random RNG => this._rng;
        //constructor 1
        public Chromosome(int numGenes, long geneLength, int? seed = null)
        {
            this._seed = seed;
            Length = geneLength;
            Genes= new int[geneLength];
            Fitness = 0;
            if(seed != null){
                this._rng = new Random((int)seed);
            }
            else{
                this._rng = new Random();
            }
            for(int i = 0; i < this.Genes.Length; i++){{
                Genes[i] = RNG.Next(1,8); // random number between 1 and 7.
            }}

        }
        //constructor 2:
        // when creating a child, the genes are already defined. Thus this overloaded constructor allows for the creation of children with predefined genes.
        public Chromosome(int numGenes, long geneLength, int[] genes, int? seed = null){
            this._seed = seed;
             if(seed != null){
                this._rng = new Random((int)seed);
            }
            else{
                this._rng = new Random();
            }
            Length = geneLength;
            Fitness = 0;
            this.Genes= genes;
        }


        //constructor 3
        public Chromosome(Chromosome chromosome): this(chromosome.NumGenes, chromosome.Length, chromosome.Genes, chromosome.Seed){
        }
        //takes another chromosome and creates and returns an array of two children chromosomes with it.
        //mutation prob: chance of any gene to mutate into a random gene (0.1 = 10%)
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            IChromosome[] children = new Chromosome[2];
            children[0] = MutateChild(GenerateChild(this, (Chromosome)spouse), mutationProb);
            children[1] = MutateChild(GenerateChild((Chromosome) spouse, this), mutationProb);
            return children;
        }
        
        //Takes 2 chromosomes and modifies the first one with genes from the sceond one with a range chosen randomly.
        private Chromosome GenerateChild(Chromosome parent1, Chromosome parent2){
            //we want to create a start and end index, these positions will be the ones whose genes are swapped with the second parent.
            int startIndP2 = RNG.Next(0, parent2.NumGenes-1); // I think the -1 should make sure that we always get at least 1 place to mutate
            int endIndP2 = RNG.Next(startIndP2, this.NumGenes);
            //genes to swap
            int[] genes = parent1.Genes;
            for(int i = startIndP2; i < endIndP2; i++){
                genes[i] = parent2[i];
            }
            Chromosome child = new Chromosome(NumGenes, parent1.Length, genes, Seed); //need to check that NumGenes and Seed get the right things.
            return child;
        }
        //takes a chromosome and mutates it at a rate of the mutationProb (0.1 = 10%)
        private IChromosome MutateChild(Chromosome child, double mutationProb){
            for(int i = 0; i < child.NumGenes; i++){
                if(RNG.NextDouble() <= mutationProb){
                    child[i] = RNG.Next(1,8);
                }
            }
            return child;
        }

        public int CompareTo(IChromosome other)
        {
            if(this.Fitness == other.Fitness){
                return 0;
            }
            else if(this.Fitness > other.Fitness){
                return 1;
            }
            else{
                return -1;
            }    
        }
    }
}