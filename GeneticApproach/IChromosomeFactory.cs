using GeneticApproach.Domain;

public interface IChromosomeFactory<Gene,Base> where Gene : class
{
    public Chromosome<Gene> ToChromosome(Base @base);
    public Base ToBase(Chromosome<Gene> chromosome);
}