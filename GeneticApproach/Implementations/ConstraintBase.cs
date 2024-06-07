using GeneticApproach.Abstract;
using GeneticApproach.Interfaces;

namespace GeneticApproach.Implementations
{
    class ConstraintBase<Gene> : AbstractConstraint<Chromosome<Gene>, Gene> where Gene : IRandomGenerable<Gene>
    {
        public override double GetConstraintValue(Chromosome<Gene> chromosome)
        {
            throw new NotImplementedException();
        }
    }
}