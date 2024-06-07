namespace GeneticApproach.Abstract
{
    public abstract class AbstractConstraint<Chromosome, Gene> 
        where Chromosome : AbstractChromosome<Gene> 
    {
        public abstract double GetConstraintValue(Chromosome chromosome);
    }
}