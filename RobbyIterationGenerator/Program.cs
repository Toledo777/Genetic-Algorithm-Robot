using System;
using System.IO;

namespace RobbyIterationGenerator
{
    class Program
    {

        static void Main(string[] args)
        {
            // string top_level_folder = "/Generated-Files/"; 
            // Defines place to store folders       
            Console.WriteLine(Environment.CurrentDirectory);
            // User input for path
            Console.Write("In what folder do you want to save the text files: ");
            var folder_name = Console.ReadLine();
            // Combine User Path to local direcotry
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, folder_name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine($"\n\n Your generated text files will be saved in \n {path}");
            }
            else
            {
                Console.WriteLine($"{folder_name} folder already exists in {path} \n Files will be created in it.");
            }

            // Input parameters for the generations
            Console.WriteLine("How many is the population Size? : ");
            int populationSize = int.Parse(Console.ReadLine());




        }
    }
}
