using System;
using System.IO;
using RobbyTheRobot;

namespace RobbyIterationGenerator
{
    class Program
    {

        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();

            // Defines place to store folders       
            Console.WriteLine(Environment.CurrentDirectory);
            // User input for path
            Console.Write("What is the folder name where you want your data to save? : ");
            var folder_name = Console.ReadLine();
            // Combine User Path to local direcotry
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, folder_name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Console.WriteLine($"Your generated text files will be saved in : \n\t\t {path}");



            // Input parameters for the generations
            int populationSize, numberOfGenes, lengthOfGene, numberOfTrials, numberOfGenerations, inputSeed;
            double mutationRate, eliteRate;
            bool noSeed = false;
            int? nullSeed = null;
            // Getting the parameters value from user
            while (true)
            {
                Console.Write("How many generations do you want? ( Recommended : 1000 ) : ");
                if (!int.TryParse(Console.ReadLine(), out numberOfGenerations))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }

                if (numberOfGenerations < 0) { Console.WriteLine("No Negative number allowed."); continue; }

                Console.Write("How many is the population Size? ( Recommended : 200 ) : ");
                if (!int.TryParse(Console.ReadLine(), out populationSize))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                if (populationSize < 0) { Console.WriteLine("No Negative number allowed."); continue; }

                Console.Write("How many Genes? ( Recommended: 243 ) : ");
                if (!int.TryParse(Console.ReadLine(), out numberOfGenes))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                if (numberOfGenes < 0) { Console.WriteLine("No Negative number allowed."); continue; }

                Console.Write("What is the Length of the Gene? ( Recommended : 7 ) : ");
                if (!int.TryParse(Console.ReadLine(), out lengthOfGene))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                if (lengthOfGene < 0) { Console.WriteLine("No Negative number allowed."); continue; }

                Console.WriteLine("Write the rate from 0-1. Where 0 is 0% and 1 is 100%");
                Console.Write("What is the mutation rate? ( Recommended : 0.05 ) : ");
                if (!double.TryParse(Console.ReadLine(), out mutationRate))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                if (mutationRate < 0 || mutationRate > 1) { Console.WriteLine("Please write the rate from 0-1. Where 0 is 0% and 1 is 100%\n"); continue; }

                Console.WriteLine("Write the rate from 0-1. Where 0 is 0% and 1 is 100%");
                Console.Write("What is the elite rate? ( Recommended : 0.1 ) : ");
                if (!double.TryParse(Console.ReadLine(), out eliteRate))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                if (eliteRate < 0 || eliteRate > 1) { Console.WriteLine("Please write the rate from 0-1. Where 0 is 0% and 1 is 100%\n"); continue; }

                Console.Write("How many trials? ( Recommended : 40 ): ");
                if (!int.TryParse(Console.ReadLine(), out numberOfTrials))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                if (numberOfTrials < 1) { Console.WriteLine("Number of Trials must at least be 1."); continue; }


                Console.Write("Number of seed? ( Press enter for none ): ");
                if (!int.TryParse(Console.ReadLine(), out inputSeed))
                {
                    if (inputSeed == 0)
                    {
                        noSeed = true;
                        break;
                    }
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                break;
            }

            // Assigns userInputed seed to nullSeed
            // Else if false - there is no seed hence nullSeed will be null.
            if (!noSeed)
            {
                nullSeed = inputSeed;
            }
            // Create RobbyTheRobot
            IRobbyTheRobot robbyRobot = Robby.createRobby(numberOfGenerations, populationSize, numberOfGenes, lengthOfGene, numberOfTrials, mutationRate, eliteRate, seed: nullSeed);

            // Start timer
            watch.Start();

            // Run Roby through grids
            for (int i = 0; i < robbyRobot.NumberOfGenerations; i++)
            {
                robbyRobot.GeneratePossibleSolutions(path);
            }

            // Stop and Print time
            watch.Stop();
            Console.WriteLine($"It took {watch.ElapsedMilliseconds} miliseconds");
            Console.WriteLine($"It took {watch.ElapsedMilliseconds / 1000} seconds");
            Console.WriteLine($"It took {watch.ElapsedMilliseconds / 1000 / 60} minutes");

        }
    }
}
