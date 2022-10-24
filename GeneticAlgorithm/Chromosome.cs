using System.Diagnostics.CodeAnalysis;
using System;

namespace GeneticAlgorithm
{
    public class Chromosome : IChromosome
    {
        private int _numGenes;
        private Random _rng;
        private int[] _genes;
        private double _fitness;
        private int? _seed = null;

        public double Fitness {
            get {return this._fitness;}
        }

        public int[] Genes {
            get{return _genes;}
        }

        public long Length{
            get;
        }

        public int this[int index] {

        get{return this._genes[index];}
        }

        //Check if we should take fitness as param or not and create overloaded constructor for it which takes a seed.
        public Chromosome(int numGenes, long geneLength, int? seed = null)
        {
            this._seed = seed;
            this._numGenes = numGenes;
            Length = geneLength;
            this._genes = new int[geneLength];
            if(seed != null){
                this._rng = new Random((int)seed);
            }
            else{
                this._rng = new Random();
            }
            for(int i = 0; i < this._genes.Length; i++){{
                _genes[i] = _rng.Next(1,8); // random number between 1 and 7.
            }}

        }
        //when creating a child, the genes are already defined.
        public Chromosome(int numGenes, long geneLength, int[] genes, int? seed = null){
            this._seed = seed;
             if(seed != null){
                this._rng = new Random((int)seed);
            }
            else{
                this._rng = new Random();
            }
            this._numGenes = numGenes;
            Length = geneLength;
            this._genes = genes;
        }



        public Chromosome(Chromosome chromosome)
        {
            // this._rng = new Random(chromosome._rng);

        }

        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            IChromosome[] children;
            //first, find the range of genes to swap.
        }
        private IChromosome generateChild(IChromosome parent1, IChromosome parent2, int startIndP2, int endIndP2){
            //genes to swap
            int[] genes = parent1.Genes;
            for(int i = startIndP2; i < endIndP2; i++){
                genes[i] = parent2[i];
            }
            Chromosome child = new Chromosome();
        }

        public int CompareTo([AllowNull] IChromosome other)
        {
            throw new System.NotImplementedException();
        }
    }
}