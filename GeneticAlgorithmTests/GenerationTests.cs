using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;

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
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 2235)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 98497)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 948545)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 53894)]
        public void TestAverageCalculation(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();
            double actualAverage = gen.AverageFitness;
            double expected = 0;

            for (int i = 0;  i < gen.NumberOfChromosomes; i++) {
                expected += gen[i].Fitness;
            }

            // computer average
            expected = expected / gen.NumberOfChromosomes;

            Assert.AreEqual(expected, actualAverage);
        }

        // test max fitness calculation
        [TestMethod]
        public void TestMaxCalculation(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();
        }

        // Test that SelectParent returns an IChromosome in the correct range
        [TestMethod]
        public void TestSelectParent(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();
        }

        // Tests the EvaluatePopulationFitness method
        [TestMethod]
        public void TestEvaluatePopFitness(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();
        }
    }
}