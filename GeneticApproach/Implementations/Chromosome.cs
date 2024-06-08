using GeneticApproach.Abstract;
using GeneticApproach.Interfaces;

namespace GeneticApproach.Implementations
{
    public class Chromosome<Gene> : AbstractChromosome<Gene>
        where Gene: IRandomGenerable<Gene>
    {
        public override List<Gene> GenesSequence { get; set; }
        private readonly int _size;
        private readonly IGeneGenerator<Gene> _geneGenerator;
        private readonly IEnumerable<AbstractConstraint<Chromosome<Gene>, Gene>> _constraints;

        public Chromosome(int size, IGeneGenerator<Gene> geneGenerator, IEnumerable<AbstractConstraint<Chromosome<Gene>,Gene>> constraints)
        {
            _size = size;
            _geneGenerator = geneGenerator;
            _constraints = constraints;
            GenesSequence = [];
            foreach (var gene in _geneGenerator.RandomGenes())
            {
                GenesSequence.Add(gene);
            }
        }

        public override double GetFitness()
        {
            return _constraints.Sum(x => x.GetConstraintValue(this));
        }

        public override void Mutate()
        {
            var rand = new Random(); 
            int firstIndex = rand.Next(0,_size), secondIndex = rand.Next(firstIndex,_size);
            for (int i = firstIndex; i < secondIndex; i++)
            {
                GenesSequence[i] = _geneGenerator.RandomGenes().First();
            }
        }
        public override int CompareTo(AbstractChromosome<Gene>? other)
        {
            return GetFitness().CompareTo(other?.GetFitness());
        }
    }
}