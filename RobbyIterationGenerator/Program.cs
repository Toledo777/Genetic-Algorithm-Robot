using System;
using System.IO;

namespace RobbyIterationGenerator
{
    class Program
    {

        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();

            // string top_level_folder = "/Generated-Files/"; 
            // // Defines place to store folders       
            // Console.WriteLine(Environment.CurrentDirectory);
            // // User input for path
            // Console.Write("In what folder do you want to save the text files: ");
            // var folder_name = Console.ReadLine();
            // // Combine User Path to local direcotry
            // var path = System.IO.Path.Combine(Environment.CurrentDirectory, folder_name);
            // if (!Directory.Exists(path))
            // {
            //     Directory.CreateDirectory(path);
            //     Console.WriteLine($"\n\n Your generated text files will be saved in \n {path}");
            // }
            // else
            // {
            //     Console.WriteLine($"{folder_name} folder already exists in {path} \n Files will be created in it.");
            // }

            watch.Start();
            // Input parameters for the generations
            bool param = true;
            int populationSize, numberOfGenes, lengthOfGene, numberOfTrials;
            double mutationRate, eliteRate;

            // Getting the parameters value from user
            while (param)
            {
                Console.WriteLine("How many is the population Size?(example : 200) : ");
                if (!int.TryParse(Console.ReadLine(), out populationSize)) continue;

                Console.WriteLine("How many Genes?(example : 10) : ");
                if (!int.TryParse(Console.ReadLine(), out numberOfGenes)) continue;

                Console.WriteLine("What is the Length of the Gene? (example : 5) : ");
                if (!int.TryParse(Console.ReadLine(), out lengthOfGene)) continue;

                Console.WriteLine("What is the mutation rate?(example : 2.0) : ");
                if (!double.TryParse(Console.ReadLine(), out mutationRate)) continue;

                Console.WriteLine("What is the elite rate?(example : 2.0) : ");
                if (!double.TryParse(Console.ReadLine(), out eliteRate)) continue;

                Console.WriteLine("How many trials? (example 200): ");
                if (!int.TryParse(Console.ReadLine(), out numberOfTrials)) continue;

                param = false;
            }

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
