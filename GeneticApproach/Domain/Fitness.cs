using GeneticApproach.Extensions;
using GeneticSharp;

namespace GeneticApproach.Domain
{
    internal class Fitness<T, Base> : IFitness where T: class
    {
        private readonly IEnumerable<BaseConstraint<Base>> _constraints;
        private readonly IChromosomeFactory<T, Base> _factory;

        public Fitness(IEnumerable<BaseConstraint<Base>> constraints, IChromosomeFactory<T, Base> factory)
        {
            _constraints = constraints;
            _factory = factory;
        }
        public double Evaluate(IChromosome chromosome)
        {
            var myChromosome = chromosome as Chromosome<T>;
            myChromosome!.ActiveConstraints = new List<string>();
            var @base = _factory.ToBase(myChromosome); 
            double fitness = 0;
            fitness = _constraints.Sum(f =>
            {
                var val = f.Evaluate(@base!);
                if (val > 0)
                    myChromosome.ActiveConstraints.Add(f.Identifier);
                return val;
            });
            if(fitness < 20)
            {
                System.Console.WriteLine(fitness);
            }
            return fitness * -1;
        }
    }
}