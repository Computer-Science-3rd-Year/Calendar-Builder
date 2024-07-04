using GeneticApproach.Extensions;
using GeneticSharp;

namespace GeneticApproach.Domain
{
    internal class GeneticController<Base, T> : SampleControllerBase where T : class
    {
        private readonly int _size;
        private readonly IEnumerable<BaseConstraint<Base>> _constraints;
        private readonly int _fitnessThresholdTermination;
        private readonly IChromosomeFactory<T, Base> _factory;
        private readonly IRandomGenerableFactory<T> _randomFactory;
        private Fitness<T,Base> m_fitness = null!;

        public GeneticController(int chromosomesGenesSequenceSize, IEnumerable<BaseConstraint<Base>> constraints, int fitnessThresholdTermination,IChromosomeFactory<T,Base> factory, IRandomGenerableFactory<T> randomFactory)
        {
            _size = chromosomesGenesSequenceSize;
            _constraints = constraints;
            _fitnessThresholdTermination = fitnessThresholdTermination;
            _factory = factory;
            _randomFactory = randomFactory;
        }
        public override IChromosome CreateChromosome()
        {
            return new Chromosome<T>(_size, _randomFactory);
        }

        public override IFitness CreateFitness()
        {
            m_fitness = new Fitness<T,Base>(_constraints, _factory);
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