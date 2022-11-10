using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
using System;

namespace ChromosomeTests
{
    [TestClass]
    public class ChromosomeTests
    {
        //Testing that children generation works: that we are indeed creating two chromosomes which match parents' attributes.
        [TestMethod]
        public void TestGenerateChildren()
        {
            int seed = 123;
            Random random = new Random(seed);
            int numberOfGenes = 243;
            Chromosome c = new Chromosome(numberOfGenes, 7, seed);
            Chromosome c2 = new Chromosome(numberOfGenes, 7, seed);
            IChromosome[] children = c.Reproduce(c2, 1);
            Assert.AreEqual(2, children.Length); //make sure we are generating 2 children
            for (int i = 0; i < children.Length; i++)
            {

                Assert.AreEqual(children[i].Length, c.Length);
                Assert.AreEqual(((Chromosome)children[i]).NumGenes, c.NumGenes); //make sure same length and number of genes are kept
            }
        }
        //Checking that an exception is thrown when recreating with a spouse that has a different number of genes.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateChildrenDiffGeneCounts()
        {
            Chromosome c = new Chromosome(222, 7);
            Chromosome c2 = new Chromosome(223, 7); //different number of genes
            IChromosome[] children = c.Reproduce(c2, 1); //expect this to fail
        }
        //Checking that an exception is thrown when recreating with a spouse that has a different Length (range of possible genes).
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateChildrenDiffGeneLength()
        {
            Chromosome c = new Chromosome(223, 8);
            Chromosome c2 = new Chromosome(223, 7);
            IChromosome[] children = c.Reproduce(c2, 1); //expect this to fail
        }

        //unsure if this needs to stay, but good to know they don't throw any exceptions.
        [TestMethod]
        public void TestConstructors()
        {
            //first constructor
            Chromosome c = new Chromosome(243, 8);
            int[] d = { 1, 2, 3, 4 };
            //second
            Chromosome c2 = new Chromosome(243, d);
            for (int i = 0; i < d.Length; i++)
            {
                Assert.AreEqual(d[i], c2[i]);
            }
            //copy constructor
            Chromosome c3 = new Chromosome(c2);
            for (int i = 0; i < d.Length; i++)
            {
                Assert.AreEqual(c3[i], c2[i]);
            }

        }

        // Testing that mutations works as expected.
        [TestMethod]
        public void TestMutation()
        {
            int seed = 123;
            Random random = new Random(seed);
            int numberOfGenes = 10;
            for (int i = 0; i < numberOfGenes; i++)
            {
                random.Next(0, 7); // when creating a chromosome, the constructor calls its _rng for each chromosome to generate.
            }
            Chromosome c = new Chromosome(numberOfGenes, 7, seed);
            Chromosome c2 = new Chromosome(numberOfGenes, 7, seed);
            IChromosome[] arr = c.Reproduce(c2, 1); //set the mutation rate to 100%
            for (int i = 0; i < ((Chromosome)arr[0]).NumGenes; i++)
            {
                //_rng.NextDouble is called when mutating the child to determine whether the mutation occurs.
                random.NextDouble();
                //our random now matches Chromosome's random which was used to mutate the child.  
                //If these values match, mutate works as intended.
                Assert.AreEqual(arr[0][i], random.Next(0, 7));
            }
        }
    }
}

