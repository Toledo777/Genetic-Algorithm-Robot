using System;
using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GeneticAlgorithmTest
    {
        int seed = 14;

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
        [DataRowAttribute(200, 243, 7, 40, 30, 20)]
        [DataRowAttribute(20, 143, 4, 40, 10, 10)]
        public void Test_GeneticAlgorithm_Properties_AreEqual(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {
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
        [DataRowAttribute(200, 243, 7, 40, 30, 20)]

        public void Test_GenerationCount_Increments(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {

            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            geneticAlgorithm.GenerationCount += 1;
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 1);
            geneticAlgorithm.GenerationCount += 1;
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 2);
            geneticAlgorithm.GenerationCount += 1;
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 3);
        }

        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 20)]
        public void Test_GenerationCount_Initalize_izZero(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRowAttribute(200, 243, 7, 40, 30, 20)]
        public void Test_GenerationCount_Cant_BeOldGen_ArgumentException(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            geneticAlgorithm.GenerationCount += 1;
            geneticAlgorithm.GenerationCount += 1;

            // Throws Exception
            geneticAlgorithm.GenerationCount = 1;
        }

        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 20)]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_GenerationCount_Cant_Increment_MoreThanOneGen_ArgumentException(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            geneticAlgorithm.GenerationCount += 1;

            // Throws Exception
            geneticAlgorithm.GenerationCount += 2;

        }

        [TestMethod]
        public void Test_Random_Seed_generatesSameNumbers()
        {
            // Test Random with seed
            Random random = new Random(seed);
            Assert.AreEqual(random.Next(200), 8);
            Assert.AreEqual(random.Next(200), 185);
            Assert.AreEqual(random.Next(200), 110);
            Assert.AreEqual(random.Next(200), 109);
            Assert.AreEqual(random.Next(200), 105);
        }

        [DataRowAttribute(40, 243, 7, 40, 30, 20)]
        [DataRowAttribute(2, 243, 7, 40, 30, 20)]
        [DataRowAttribute(124, 243, 7, 40, 30, 20)]
        [TestMethod]
        public void Test_GenerateGeneration_First_Time(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration generatedGeneration = geneticAlgorithm.GenerateGeneration();
            // Checks that current generation is set
            Assert.AreEqual(generatedGeneration, geneticAlgorithm.CurrentGeneration);
            // Checks that Number of Chromsomes is equal to population size
            Assert.AreEqual(generatedGeneration.NumberOfChromosomes, populationSize);
            // Checks that Generation Count is 1
            Assert.AreEqual(geneticAlgorithm.GenerationCount, 1);

            // Compare generated generation to current geeeration
            for (int i = 0; i < populationSize; i++)
            {
                Assert.AreEqual(generatedGeneration[i], geneticAlgorithm.CurrentGeneration[i]);
            }
        }

        [DataRowAttribute(200, 243, 7, 40, 30, 20)]
        [TestMethod]
        public void Test_GenerateGeneration_First_Time_Correct_Chromosome_Values(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration generatedGeneration = geneticAlgorithm.GenerateGeneration();
            IChromosome chromosome = generatedGeneration[populationSize - 42];
            // Checks that Generation of the first generation initalized the chromsome correctly
            Assert.AreEqual(chromosome, generatedGeneration[populationSize - 42]);
            Assert.AreEqual(chromosome, geneticAlgorithm.CurrentGeneration[populationSize - 42]);
            Assert.AreEqual(chromosome.Genes.Length, numberOfGenes);
            Assert.AreEqual(chromosome.Length, lengthOfGene);
        }


        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 20)]
        public void Test_GenerateGeneration_Second_Time(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials)
        {
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

        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 14)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 7)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 2)]


        public void Test_GenerateGeneration_Compare_Two_Generations_Same_Seed(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int? seed)
        {
            // Create two Genetic Algorithms with the same seed
            GeneticAlgorithm.GeneticAlgorithm genetic1 = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            GeneticAlgorithm.GeneticAlgorithm genetic2 = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);

            // Generate two generations
            IGeneration generation1 = genetic1.GenerateGeneration();
            IGeneration generation2 = genetic2.GenerateGeneration();

            // Compare the two generations
            for (int i = 0; i < numberOfGenes; i++)
            {
                Assert.AreEqual(generation1[0][i], generation2[0][i]);
            }

                for (int i = 0; i < numberOfGenes; i++)
            {
                Assert.AreEqual(generation1[populationSize-1][i], generation2[populationSize-1][i]);
            }

        }
    }
}