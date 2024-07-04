using GeneticApproach.Domain;
using GeneticApproach.Extensions;
using GeneticSharp;

namespace GeneticApproach
{
    public interface IGeneticService<T, Base> where T : class 
    {
        IEnumerable<GeneticResults> Evolution(IEnumerable<BaseConstraint<Base>> constraints, GeneticOptions options, IChromosomeFactory<T, Base> factory, IRandomGenerableFactory<T> randomFactory);
    }

    public class GeneticService<T, Base> : IGeneticService<T, Base> where T : class
    {
        public IEnumerable<GeneticResults> Evolution(IEnumerable<BaseConstraint<Base>> constraints, GeneticOptions options, IChromosomeFactory<T, Base> factory,  IRandomGenerableFactory<T> randomFactory)
        {
            var sampleController = new GeneticController<Base, T>(options.ChromosomeLength, constraints, 0, factory,randomFactory);
            sampleController.Initialize();

            Console.WriteLine("Starting...");

            var selection = sampleController.CreateSelection();
            var crossover = sampleController.CreateCrossover();
            var mutation = sampleController.CreateMutation();
            var fitness = sampleController.CreateFitness();
            var population = new Population(100, 200, sampleController.CreateChromosome());
            population.GenerationStrategy = new PerformanceGenerationStrategy();

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = sampleController.CreateTermination();

            var terminationName = ga.Termination.GetType().Name;

            ga.GenerationRan += delegate
            {
                var bestChromosome = ga.Population.BestChromosome;
                Console.WriteLine("Termination: {0}", terminationName);
                Console.WriteLine("Generations: {0}", ga.Population.GenerationsNumber);
                Console.WriteLine("Fitness: {0,10}", bestChromosome.Fitness);
                Console.WriteLine("Time: {0}", ga.TimeEvolving);
                Console.WriteLine("Speed (gen/sec): {0:0.0000}", ga.Population.GenerationsNumber / ga.TimeEvolving.TotalSeconds);
                sampleController.Draw(bestChromosome);
            };

            try
            {
                sampleController.ConfigGA(ga);
                ga.Start();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ResetColor();
                Console.ReadKey();
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("Evolved.");
            Console.ResetColor();
            var best = ga.Population.BestChromosome;
            yield return new GeneticResults()
            {
                Solution = best.GetGenes()
            };
        }
    }
}