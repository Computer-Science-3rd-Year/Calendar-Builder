using GeneticApproach.Extensions;
using GeneticSharp;
internal class Fitness<T> : IFitness
    where T : IRandomGenerable<T>
{
    private readonly IEnumerable<GeneticConstraint<T>> _constraints;
    public Fitness(IEnumerable<GeneticConstraint<T>> constraints)
    {
        _constraints = constraints;
    }
    public double Evaluate(IChromosome chromosome)
    {
        var myChromosome  = chromosome as Chromosome<T>;
        myChromosome!.ActiveConstraints = new List<string>();
        double fitness = 0; 
        fitness = _constraints.Sum(f => {
            var val = f.Evaluate(myChromosome!);
            if (val > 0)
                myChromosome.ActiveConstraints.Add(f.Identifier);
            return val;
        }); 
        return fitness * -1;
    }
}

