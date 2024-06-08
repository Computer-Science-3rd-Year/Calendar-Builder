namespace GeneticApproach.Interfaces
{
    public interface IGeneGenerator<Gene> where Gene : IRandomGenerable<Gene>
    {
        public IEnumerable<Gene> RandomGenes();
    }
}