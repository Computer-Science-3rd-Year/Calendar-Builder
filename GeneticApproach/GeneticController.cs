using GeneticApproach.Extensions;
using GeneticSharp;

class GeneticController<T> : SampleControllerBase
    where T : IRandomGenerable<T>
{
    private readonly int _size;
    private readonly IEnumerable<GeneticConstraint<T>> _constraints;
    private Fitness<T> m_fitness;

    public GeneticController(int chromosomesGenesSequenceSize,IEnumerable<GeneticConstraint<T>> constraints)
    {
        _size = chromosomesGenesSequenceSize;
        _constraints = constraints;
    }
    public override IChromosome CreateChromosome()
    {
        return new Chromosome<T>(_size);  
    }

    public override IFitness CreateFitness()
    {
        m_fitness = new Fitness<T>(_constraints);
        return m_fitness;
    }

    public override void Draw(IChromosome bestChromosome)
    {
        var c = bestChromosome as Chromosome<T>; 
        c.Draw();
        Console.WriteLine();
        Console.WriteLine("Active Constraints:");
        c.ActiveConstraints.ForEach(Console.WriteLine); 
    }
    
    public override ITermination CreateTermination()
    {
        return new FitnessThresholdTermination(0);
    }
}
