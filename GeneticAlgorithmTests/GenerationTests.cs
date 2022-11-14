using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
using System;
namespace GeneticAlgorithmTests
{

    [TestClass]
    public class GenerationTests
    {
        public static FitnessEventHandler handler;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            handler = Fitness;
            double Fitness(IChromosome chromosome, IGeneration generation)
            {
                return 2.0;
            }
        }

        // test the average calculation
        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 2235, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 98497, true)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 948545, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 53894, true)]
        public void TestAverageCalculation(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed, bool regenerate)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();

            // generates a new generation based of the previous one, uses the 2nd generation constructor
            if (regenerate)
                gen = geneticAlgorithm.GenerateGeneration();

            double actualAverage = gen.AverageFitness;
            double expected = 0;

            for (int i = 0; i < gen.NumberOfChromosomes; i++)
            {
                expected += gen[i].Fitness;
            }

            // computer average
            expected = expected / gen.NumberOfChromosomes;

            Assert.AreEqual(expected, actualAverage);
        }

        // test max fitness calculation with 1st gen
        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 223545, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 345497, true)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 465, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 94, true)]
        public void TestMaxCalculation(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed, bool regenerate)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();

            // generates a new generation based of the previous one, uses the 2nd generation constructor
            if (regenerate)
                gen = geneticAlgorithm.GenerateGeneration();

            double maxActual = gen.MaxFitness;
            double expected = gen[0].Fitness;

            // loop chromosomes to get max starting at index 1
            for (int i = 1; i < gen.NumberOfChromosomes; i++)
            {
                if (gen[i].Fitness > expected)
                {
                    expected = gen[i].Fitness;
                }
            }

            Assert.AreEqual(expected, maxActual);
        }

        // Test that SelectParent returns an IChromosome in the correct range
        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 223545, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 345497, true)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 465, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 94, true)]
        public void TestSelectParent(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed, bool regenerate)
        {

            int parentRange = 20;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();

            // generates a new generation based of the previous one, uses the 2nd generation constructor
            if (regenerate)
                gen = geneticAlgorithm.GenerateGeneration();

            IChromosome selectedParent = (gen as Generation).SelectParent();

            // there should be at most 19 better parents
            int betterParent = 0;

            for (int i = 0; i < gen.NumberOfChromosomes; i++)
            {
                if (gen[i].Fitness > selectedParent.Fitness)
                {
                    // count amount off chromosomes with higher fitness then parent
                    betterParent++;
                }
            }

            // check to make sure that there is less then 20 better parents
            // test that select parent is getting parents from the correct range
            Assert.IsTrue(betterParent < parentRange);
        }

        // Tests the EvaluatePopulationFitness method
        [TestMethod]
        [DataRowAttribute(200, 243, 7, 40, 30, 21, 1111111, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 2, 63497, true)]
        [DataRowAttribute(200, 243, 7, 40, 30, 1, 43555, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 7, 96688, true)]
        public void TestEvaluatePopFitness(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed, bool regenerate)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();

            if (regenerate)
                gen = geneticAlgorithm.GenerateGeneration();

            (gen as Generation).EvaluateFitnessOfPopulation();

            // 2.0 is the fitness returned by the mock handler
            double expected = populationSize * 2.0;
            double actualSum = 0;
            // calculate sum of all fitness to verify that handler is being called correctly
            for (int i = 0; i < gen.NumberOfChromosomes; i++)
            {
                actualSum += gen[i].Fitness;
            }

            Assert.AreEqual(expected, actualSum);
        }

        // not sure if this test is needed, if GeneticAlgorithm prevents this error then the exception will never be called

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [DataRowAttribute(200, 243, 7, 40, 30, 0, 1111111, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 0, 63497, true)]
        public void TestEvaluatePopFitnessException(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed, bool regenerate)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);

            IGeneration gen = geneticAlgorithm.GenerateGeneration();
            if (regenerate)
                gen = geneticAlgorithm.GenerateGeneration();

            (gen as Generation).EvaluateFitnessOfPopulation();
        }
    }
}