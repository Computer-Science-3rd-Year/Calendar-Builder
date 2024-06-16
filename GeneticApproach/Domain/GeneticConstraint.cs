using GeneticApproach.Extensions;

namespace GeneticApproach.Domain
{
    public class GeneticConstraint<T> where T : IRandomGenerable<T>
    {
        private readonly Func<Chromosome<T>, double> _func;
        public string Identifier { get; private set; }

        public GeneticConstraint(string identifier, Func<Chromosome<T>, double> func)
        {
            Identifier = identifier;
            _func = func;
        }
        public double Evaluate(Chromosome<T> chromosome)
        {
            return _func(chromosome);
        }
    }
}