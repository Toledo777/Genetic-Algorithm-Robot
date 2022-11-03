using System;
using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GeneticAlgorithmTest
    {
        public static FitnessEventHandler handler;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            handler = Fitness;
            double Fitness(IChromosome chromosome, IGeneration generation)
            {
                return 0.0;
            }
        }

        // Test GenetiCalgorithm.cs
        [TestMethod]
        public void Test_GeneticAlgorithm_Properties_AreEqual()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 30;
            int numberOfTrials = 20;
            int? seed = 14;

            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            Assert.AreEqual(geneticAlgorithm.PopulationSize, populationSize);
            Assert.AreEqual(geneticAlgorithm.NumberOfGenes, numberOfGenes);
            Assert.AreEqual(geneticAlgorithm.LengthOfGene, lengthOfGene);
            Assert.AreEqual(geneticAlgorithm.MutationRate, mutationRate);
            Assert.AreEqual(geneticAlgorithm.EliteRate, eliteRate);
            Assert.AreEqual(geneticAlgorithm.NumberOfTrials, numberOfTrials);
            Assert.AreEqual(geneticAlgorithm.FitnessCalculation, handler);
            Assert.AreEqual(geneticAlgorithm.Seed, seed);
        }

    }
}