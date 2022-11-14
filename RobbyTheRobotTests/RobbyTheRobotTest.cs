using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;

namespace RobbyTheRobotTests
{
    [TestClass]
    public class RobbyTheRobotTest
    {
        const string file_name = "Test.txt";
        const int NUMBER_OF_ACTIONS = 200;

        [TestMethod]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, -48, 10)]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, 8, 5)]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, 09, 25)]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, 7, 8)]
        public void Test_Generate_Random_Test_Grid_Cans_Fill_Half_Grid(int numberOfGenerations, int populationSize,
         int numberOfGenes, int lengthOfGene, int numberOfTrials,
         double mutationRate, double eliteRate, int? seed = null, int gridSize = 10)
        {

            var robby = new RobbyTheRobot.RobbyTheRobot(numberOfGenerations, populationSize, numberOfGenes, lengthOfGene, numberOfTrials, mutationRate, eliteRate, seed, gridSize: gridSize);
            var testGrid = robby.GenerateRandomTestGrid();

            var countCan = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (testGrid[i, j] == ContentsOfGrid.Can)
                    {
                        countCan++;
                    }
                }
            }

            Assert.AreEqual((gridSize * gridSize) / 2, countCan);
        }

        [TestMethod]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, 3241, 10)]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, -321, 23)]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, 1, 3)]
        [DataRow(1000, 200, 243, 7, 10, 0.05, 0.1, 132, 16)]
        public void Test_Generate_Random_Test_Grid_Half_Grid_Empty(int numberOfGenerations, int populationSize,
        int numberOfGenes, int lengthOfGene, int numberOfTrials,
        double mutationRate, double eliteRate, int? seed = null, int gridSize = 10)
        {

            var robby = new RobbyTheRobot.RobbyTheRobot(numberOfGenerations, populationSize, numberOfGenes, lengthOfGene, numberOfTrials, mutationRate, eliteRate, seed, gridSize: gridSize);
            var testGrid = robby.GenerateRandomTestGrid();

            var emptySpace = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (testGrid[i, j] == ContentsOfGrid.Empty)
                    {
                        emptySpace++;
                    }
                }
            }


            Assert.AreEqual((gridSize * gridSize) - ((gridSize * gridSize) / 2), emptySpace);
        }

        [TestMethod]
        [DataRow(10, 200, 243, 7, 10, 0.05, 0.1, 3241, 10)]
        public void Test_Generate_Possible_Solutions_Written_to_file(int numberOfGenerations, int populationSize,
                        int numberOfGenes, int lengthOfGene, int numberOfTrials,
                        double mutationRate, double eliteRate, int? seed = null, int gridSize = 10)
        {

            var robby = new RobbyTheRobot.RobbyTheRobot(numberOfGenerations, populationSize, numberOfGenes, lengthOfGene, numberOfTrials, mutationRate, eliteRate, seed, gridSize: gridSize);

            robby.GeneratePossibleSolutions(file_name);

            // Check that file was created
            Assert.IsTrue(System.IO.File.Exists(file_name));
            System.IO.File.Delete(file_name);
            // Check that file was deleted
            Assert.IsFalse(System.IO.File.Exists(file_name));
        }

        [TestMethod]
        [DataRow(10, 200, 243, 7, 10, 0.05, 0.1, 3241, 10)]
        public void Test_Generate_Possible_Solutions_Written_to_file_Corecct_Data(int numberOfGenerations, int populationSize,
                                                            int numberOfGenes, int lengthOfGene, int numberOfTrials,
                                                            double mutationRate, double eliteRate, int? seed = null, int gridSize = 10)
        {

            var robby = new RobbyTheRobot.RobbyTheRobot(numberOfGenerations, populationSize, numberOfGenes, lengthOfGene, numberOfTrials, mutationRate, eliteRate, seed, gridSize: gridSize);

            robby.GeneratePossibleSolutions(file_name);

            // Check file Values
            if (System.IO.File.Exists(file_name))
            {
                var lines = System.IO.File.ReadAllLines(file_name);
                foreach (var line in lines)
                {
                    var values = line.Split(',');
                    Assert.AreEqual(245, values.Length);
                    Assert.AreEqual(NUMBER_OF_ACTIONS, values[1].Length);
                }
                System.IO.File.Delete(file_name);

            }
            else
            {
                Assert.Fail("File not created");
            }

        }
    }
}
