using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;

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
        Chromosome c = new Chromosome(2,3);
            int[] d = {1,2,3,4};
        //second
        Chromosome c2 = new Chromosome(4,d);
        for(int i = 0; i < d.Length; i++){
             Assert.AreEqual(d[i], c2[i]);
        }
        //copy constructor
        Chromosome c3 = new Chromosome(c2);
            for(int i = 0; i < d.Length; i++){
                Assert.AreEqual(c3[i], c2[i]);
        }

    }
    }
}

