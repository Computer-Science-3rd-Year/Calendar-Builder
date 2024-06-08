namespace GeneticApproach.Abstract
{
    public abstract class AbstractGeneticPopulation<Gene, Chromosome> where Chromosome: AbstractChromosome<Gene>
    {
        public abstract List<Chromosome> Chromosomes { get; set; }
        public abstract (Chromosome FirstChild, Chromosome SecondChild) TwoPointCrossOver(Chromosome first, Chromosome second);
        public abstract AbstractGeneticPopulation<Gene,Chromosome> GetRandomPopulation(int size);
        public abstract void Evolve(); 
    }
}

