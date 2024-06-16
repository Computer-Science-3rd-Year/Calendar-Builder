using GeneticApproach.Domain;
using GeneticApproach.Extensions;

public interface IChromosomeFactory<Gene,Base> where Gene: IRandomGenerable<Gene>
{
    public Chromosome<Gene> ToChromosome(Base @base);
    public Base ToBase(Chromosome<Gene> chromosome);
}