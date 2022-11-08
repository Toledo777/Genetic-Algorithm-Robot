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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GenerationCount_Increments()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 30;
            int numberOfTrials = 20;
            int? seed = 14;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            geneticAlgorithm.GenerationCount += 1;
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 1);
            geneticAlgorithm.GenerationCount += 1;
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 2);
            geneticAlgorithm.GenerationCount += 1;
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 3);

            // Throws Exception
            geneticAlgorithm.GenerationCount = 0;
        }

        public void Test_GenerationCount_Initalize_izZero()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 30;
            int numberOfTrials = 20;
            int? seed = 14;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GenerationCount_Cant_BeOldGen_ArgumentException()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 30;
            int numberOfTrials = 20;
            int? seed = 14;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            geneticAlgorithm.GenerationCount += 1;
            geneticAlgorithm.GenerationCount += 1;

            // Throws Exception
            geneticAlgorithm.GenerationCount = 1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GenerationCount_Cant_Increment_MoreThanOneGen_ArgumentException()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 30;
            int numberOfTrials = 20;
            int? seed = 14;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            geneticAlgorithm.GenerationCount += 1;

            // Throws Exception
            geneticAlgorithm.GenerationCount += 2;

        }

        [TestMethod]
        public void Test_Random_Seed_generatesSameNumbers()
        {
            int seed = 14;
            // Test Random with seed
            Random random = new Random(seed);
            Assert.AreEqual(random.Next(200), 8);
            Assert.AreEqual(random.Next(200), 185);
            Assert.AreEqual(random.Next(200), 110);
            Assert.AreEqual(random.Next(200), 109);
            Assert.AreEqual(random.Next(200), 105);
        }

        [TestMethod]
        public void Test_GenerateGeneration_First_Time()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 30;
            int numberOfTrials = 20;
            int? seed = 14;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration generatedGeneration = geneticAlgorithm.GenerateGeneration();
            // Checks that current generation is set
            Assert.AreEqual(generatedGeneration, geneticAlgorithm.CurrentGeneration);
            // Checks that Number of Chromsomes is equal to population size
            Assert.AreEqual(generatedGeneration.NumberOfChromosomes, populationSize);
            // Checks that Generation Count is 1
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 1);
        }

        [TestMethod]
        public void Test_GenerateGeneration_First_Time_Correct_Chromosome_Values()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 30;
            int numberOfTrials = 20;
            int? seed = 14;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration generatedGeneration = geneticAlgorithm.GenerateGeneration();
            IChromosome chromosome = generatedGeneration[populationSize - 42];
            // Checks the same chromosome is returned
            Assert.AreEqual(chromosome, generatedGeneration[populationSize - 42]);
            Assert.AreEqual(chromosome.Length, lengthOfGene);
        }


        [TestMethod]
        public void Test_GenerateGeneration_Second_Time()
        {
            int populationSize = 200;
            int numberOfGenes = 243;
            int lengthOfGene = 7;
            double mutationRate = 40;
            double eliteRate = 10;
            int numberOfTrials = 20;
            int? seed = 14;
            // Try to create Generation
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration generation = geneticAlgorithm.GenerateGeneration();
            // Generate Second Generation
            generation = geneticAlgorithm.GenerateGeneration();
            // Checks that current generation is set
            Assert.AreEqual(generation, geneticAlgorithm.CurrentGeneration);
            // Checks that Number of Chromsomes is equal to population size
            Assert.AreEqual(generation.NumberOfChromosomes, populationSize);
            // Checks that Generation Count is 1
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 2);
        }
    }
}