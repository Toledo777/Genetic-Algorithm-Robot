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
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 2235, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 98497, true)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 948545, false)]
        [DataRowAttribute(200, 243, 7, 40, 30, 20, 53894, true)]
        public void TestAverageCalculation(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, int seed, bool regenerate)
        {
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, handler, seed);
            IGeneration gen = geneticAlgorithm.GenerateGeneration();

            // generates a new generation based of the previous one, uses the 2nd generation constructor
            if (regenerate) {

                gen = geneticAlgorithm.GenerateGeneration();
            }

            double actualAverage = gen.AverageFitness;
            double expected = 0;

            for (int i = 0;  i < gen.NumberOfChromosomes; i++) {
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
            if (regenerate) {

                gen = geneticAlgorithm.GenerateGeneration();
            }

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