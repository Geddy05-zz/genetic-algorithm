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
			double crossoverRate;
			double.TryParse (Console.ReadLine (), out crossoverRate);
			Console.WriteLine ("What is the mutationRate");
			double mutationRate;
			double.TryParse(Console.ReadLine(),out mutationRate);
			bool elitism = false;
			Console.WriteLine ("What is the populationSize");
			int populationSize = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine ("What is the numIterations");
			int numIterations= Convert.ToInt32(Console.ReadLine());

			String individual = createIndividual ();
			double fitness = computeFitness (individual);


			Console.WriteLine (fitness);
			GeneticAlgorithm<string> fakeProblemGA = new GeneticAlgorithm<string>(crossoverRate, mutationRate, elitism, populationSize, numIterations); // CHANGE THE GENERIC TYPE (NOW IT'S INT AS AN EXAMPLE) AND THE PARAMETERS VALUES
//			var solution = fakeProblemGA.Run(createIndividual, computeFitness, selectTwoParents, crossover, mutation); 
//            Console.WriteLine("Solution: ");
//            Console.WriteLine(solution);

        }

		public static string createIndividual(){
			string individual = "";
			Random random = new Random ();
			for (int i = 0; i < 5; i++) {
				individual = individual + random.Next(0,2);
			}
			return individual;
		}

		public static double computeFitness(string individual){
			int x = 7;
			double y = -Math.Pow(x, 2) + 7 * x;
			return y;
		}

		public static Func<Tuple<string,string>> selectTwoParents(string[] individuals, double[] fitnesses){
			return null;
		}

		public static Tuple<string, string> crossover(Tuple<string,string> individuals){
			return individuals;
		}

		public static string mutation(string individual, double mutationRate){
			return individual;
		}
    }
}
