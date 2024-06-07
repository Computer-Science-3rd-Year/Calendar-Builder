using GeneticApproach.Interfaces;

namespace GeneticApproach.Abstract
{
    public abstract class AbstractChromosome<Gene>
    {
        public abstract List<Gene> GenesSequence { get; set; }
        public abstract void Mutate();
        public abstract double GetFitness(IEnumerable<AbstractConstraint<AbstractChromosome<Gene>,Gene>> constraints);
    }
}



