using GeneticApproach.Extensions;
using GeneticSharp;

namespace GeneticApproach.Domain
{
    class GeneticController<T> : SampleControllerBase
        where T : IRandomGenerable<T>
    {
        private readonly int _size;
        private readonly IEnumerable<GeneticConstraint<T>> _constraints;
        private readonly int _fitnessThresholdTermination;
        private Fitness<T> m_fitness = null!;

        public GeneticController(int chromosomesGenesSequenceSize, IEnumerable<GeneticConstraint<T>> constraints, int fitnessThresholdTermination)
        {
            _size = chromosomesGenesSequenceSize;
            _constraints = constraints;
            _fitnessThresholdTermination = fitnessThresholdTermination;
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
            return new FitnessThresholdTermination(_fitnessThresholdTermination);
        }
    }
}