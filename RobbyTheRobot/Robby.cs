namespace RobbyTheRobot
{
    public static class Robby{
        public static IRobbyTheRobot createRobby(int numberOfGenerations, int populationSize, int numberOfGenes, int lengthOfGene, int numberOfTrials, 
                                                 double mutationRate, double eliteRate,int? seed = null){
            return new RobbyTheRobot(numberOfGenerations, populationSize, numberOfGenes, lengthOfGene, numberOfTrials, mutationRate , eliteRate, seed);
        }
    }
}