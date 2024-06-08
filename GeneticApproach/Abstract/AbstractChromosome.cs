namespace GeneticApproach.Abstract
{
    public abstract class AbstractChromosome<Gene> : IComparable<AbstractChromosome<Gene>>
    {
        public abstract List<Gene> GenesSequence { get; set; }
        public abstract void Mutate();
        public abstract double GetFitness();
        public abstract int CompareTo(AbstractChromosome<Gene>? other);
    }
}



