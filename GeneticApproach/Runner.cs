

using GeneticApproach.Extensions;
using GeneticSharp;

class Runner
{
    class testClass : IRandomGenerable<testClass>
    {
        public static testClass GenerateRandom()
        {
            return new testClass(){
                Value = new Random().Next(0,6)
            };
        }

        public static void Draw(testClass target)
        {
            Console.Write(target.Value + " "); 
        }

        public int Value { get; set; }
    }
    public Runner()
    {
        Run(); 
    }

    public void Run()
    {
        Console.SetError(TextWriter.Null);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("GeneticSharp - ConsoleApp");
        Console.ResetColor();
        Console.WriteLine("Select the sample:");

        List<GeneticConstraint<testClass>> cons = new List<GeneticConstraint<testClass>>(){
            new GeneticConstraint<testClass>("not even", x => {
                double fitness = 0.0;
                var arr = x.GetGenes();
                for (int i = 0; i < arr.Length; i++)
                {
                    if((arr[i].Value as testClass).Value % 2 == 0)
                    {
                        fitness += 1;
                    }
                }
                return fitness;            
            }),
            new GeneticConstraint<testClass>("not consecutive", x => {
                double fitness = 0.0;
                var arr = x.GetGenes();
                for (int i = 0; i < arr.Length-1; i++)
                {
                    if(Math.Abs((arr[i].Value as testClass).Value - (arr[i+1].Value as testClass).Value) <= 1)
                    {
                        fitness += 1;
                    }
                }
                return fitness;
            }),
        };

        var sampleController = new GeneticController<testClass>(20,cons);
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
            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine();
        Console.WriteLine("Evolved.");
        Console.ResetColor();
        Console.ReadKey();
        Run();
    }
}
