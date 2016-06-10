using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            /* FUNCTIONS TO DEFINE (for each problem):
            Func<Ind> createIndividual;                                 ==> input is nothing, output is a new individual
            Func<Ind,double> computeFitness;                            ==> input is one individual, output is its fitness
            Func<Ind[],double[],Func<Tuple<Ind,Ind>>> selectTwoParents; ==> input is an array of individuals (population) and an array of corresponding fitnesses, output is a function which (without any input) returns a tuple with two individuals (parents)
            Func<Tuple<Ind, Ind>, Tuple<Ind, Ind>> crossover;           ==> input is a tuple with two individuals (parents), output is a tuple with two individuals (offspring/children)
            Func<Ind, double, Ind> mutation;                            ==> input is one individual and mutation rate, output is the mutated individual
            */

			Console.WriteLine ("What is the crossoverRate");
			double crossoverRate = 0.5;
//			double.TryParse (Console.ReadLine (), out crossoverRate);
			Console.WriteLine ("What is the mutationRate");
			double mutationRate = 0.5;
//			double.TryParse(Console.ReadLine(),out mutationRate);
			bool elitism = true;
			Console.WriteLine ("What is the populationSize");
			int populationSize = 4;
//				Convert.ToInt32(Console.ReadLine());

			Console.WriteLine ("What is the numIterations");
			int numIterations= 20;
//				Convert.ToInt32(Console.ReadLine());

			String individual = createIndividual ();
			double fitness = computeFitness (individual);


			Console.WriteLine (fitness);
			GeneticAlgorithm<string> fakeProblemGA = new GeneticAlgorithm<string>(crossoverRate, mutationRate, elitism, populationSize, numIterations); // CHANGE THE GENERIC TYPE (NOW IT'S INT AS AN EXAMPLE) AND THE PARAMETERS VALUES
			var solution = fakeProblemGA.Run(createIndividual, computeFitness, selectTwoParents, crossover, mutation); 

			Console.WriteLine("Fitness: ");
			Console.WriteLine(computeFitness(solution.Item1));

			Console.WriteLine("Solution: ");
			Console.WriteLine(Convert.ToInt32 (solution.Item1,2));
        }

		public static string createIndividual(){
			string individual = "";
			Random random = new Random();
			for (int i = 0; i < 5; i++) {
				individual = individual + random.Next(0,2);
			}
			System.Threading.Thread.Sleep(50);
			return individual;
		}

		public static double computeFitness(string individual){
			int x = Convert.ToInt32 (individual,2);
			double y = -Math.Pow(x, 2) + 7 * x;
			return y;
		}

		public static Func<Tuple<string,string>> selectTwoParents(string[] individuals, double[] fitnesses){
			int lenght = individuals.Length;
			List<string> tempIndividuals = individuals.ToList();
//			List<double> tempFitnesses = fitnesses.ToList();
			List<double> tempFitnesses = new List<double>();
			double[] probability = new double[lenght];
			string[] parents = new string[2];
			Random random = new Random ();
			double lowestFitness = 0;

			foreach (double fitness in fitnesses) {
				if (fitness < lowestFitness) {
					lowestFitness = fitness;
				}
			}

			foreach (double fitness in fitnesses) {
				tempFitnesses.Add (fitness + Math.Abs(lowestFitness));
			}

			// sum Of Fitness is going wrong;
			for(int i = 0; i < 2; i++){
				double sumOfFitness = 0.0;

				foreach (double fitness in tempFitnesses) {
					sumOfFitness += fitness;
				}

				for (int j = 0; j < tempFitnesses.Count(); j++) {
					probability[j] = (tempFitnesses[j]/sumOfFitness);
				}
				double rollete = random.NextDouble();
				double lastProb = 0.0;
				for(int j = 0; j < tempFitnesses.Count(); j++){
					if (rollete >= lastProb && rollete <= lastProb+probability[j]) {
						parents [i] = tempIndividuals [j];
//							int num = j;
//						tempIndividuals.Remove(tempIndividuals[j]);
//						tempFitnesses.Remove(tempFitnesses[j]);
						break;
					}
					lastProb += probability [j];
				}
			}
			if (parents [0] == null) {
				parents [0] = individuals [1];
			}
			if (parents [1] == null) {
				parents [1] = individuals [0];
			}
			return () => Tuple.Create(parents[0],parents[1]);
		}

		public static Tuple<string, string> crossover(Tuple<string,string> individuals){
			string crossover1 = individuals.Item1.Substring (individuals.Item1.Length-3);
			string crossover2 = individuals.Item2.Substring(individuals.Item2.Length-3);

			string individual1 = individuals.Item1.Substring (0, 2) + crossover2;
			string individual2 = individuals.Item2.Substring (0, 2) + crossover1;

			return Tuple.Create (individual1, individual2);
		}

		public static string mutation(string individual, double mutationRate){
			Random random = new Random ();
			double mutation = random.NextDouble();
			if (mutation <= mutationRate) {
				
				int placeMutation = random.Next (0, individual.Length - 1);
				System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(individual);
				Char value = strBuilder [placeMutation];
				if(value == '1') {
					strBuilder [placeMutation] = '0';
				}else{
					strBuilder [placeMutation] = '1';
				}
				return strBuilder.ToString();
			}
			return individual;
		}
    }
}
