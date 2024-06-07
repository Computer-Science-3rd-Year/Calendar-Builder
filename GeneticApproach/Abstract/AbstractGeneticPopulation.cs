namespace GeneticApproach.Abstract
{
    public abstract class AbstractGeneticPopulation<T, C> where C: AbstractChromosome<T>
    {
        public abstract List<C> Chromosomes { get; set; }
        public abstract (C FirstChild, C SecondChild) TwoPointCrossOver(C first, C second);
        public abstract AbstractGeneticPopulation<T,C> GetRandomPopulation(int size);
    }
}

