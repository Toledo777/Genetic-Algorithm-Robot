using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
using System;

namespace ChromosomeTests
{
    [TestClass]
    public class UnitTest1
    {
        // [TestMethod]
        // public void TestMethod1()
        // {
            
        //     Chromosome child = c.GenerateChild(c,c2);
        // }
    
    [TestMethod]
    public void TestConstructors()
    {
        //first constructor
        Chromosome c = new Chromosome(243,8);
            int[] d = {1,2,3,4};
        //second
        Chromosome c2 = new Chromosome(243,d);
        for(int i = 0; i < d.Length; i++){
             Assert.AreEqual(d[i], c2[i]);
        }
        //copy constructor
        Chromosome c3 = new Chromosome(c2);
            for(int i = 0; i < d.Length; i++){
                Assert.AreEqual(c3[i], c2[i]);
        }

    }
    [TestMethod]
    public void TestMutationWorks()
    {
        int seed = 123;
        Random random = new Random(seed);
        int numberOfGenes = 10;
        for(int i = 0; i < numberOfGenes; i++){
            random.Next(0,7);
        }
        Chromosome c = new Chromosome(numberOfGenes,7, seed);
        Chromosome c2 = new Chromosome(numberOfGenes, 7, seed);
        IChromosome[] arr = c.Reproduce(c2, 1);
        for(int i = 0; i < arr[0].Length; i++){
            random.NextDouble();
            Assert.AreEqual(c2[i], random.Next(0,7));   
        }
        }
    }
}

