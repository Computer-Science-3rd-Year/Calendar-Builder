using GeneticApproach.Abstract;
using GeneticApproach.Interfaces;

namespace GeneticApproach.Implementations
{
    public class GeneticPopulation<Gene> : AbstractGeneticPopulation<Gene,Chromosome<Gene>> where Gene : IRandomGenerable<Gene>
    {
        private readonly IGeneGenerator<Gene> _geneGenerator;
        public override List<Chromosome<Gene>> Chromosomes { get; set; }
        public GeneticPopulation(IGeneGenerator<Gene> geneGenerator)
        {
            Chromosomes = [];
            _geneGenerator = geneGenerator;
        }
        public override AbstractGeneticPopulation<Gene,Chromosome<Gene>> GetRandomPopulation(int populationSize)
        {   
            for (int i = 0; i < populationSize; i++)
            {
                Chromosomes.Add(new Chromosome<Gene>(10,_geneGenerator)); //TODO: set the right number  
            }
            return this; 
        }

        public override (Chromosome<Gene> FirstChild, Chromosome<Gene> SecondChild) TwoPointCrossOver(Chromosome<Gene> firstChild,Chromosome<Gene> secondChild)
        {   
            if (firstChild.GenesSequence.Count != secondChild.GenesSequence.Count)
            {
                throw new ArgumentException("The tow chromosomes sequences should match their sizes.");
            }
            int size = firstChild.GenesSequence.Count;
            var rand = new Random(); 
            int firstIndex = rand.Next(0,size), secondIndex = rand.Next(firstIndex,size);
            for (int i = firstIndex; i < secondIndex; i++)
            {
                (secondChild.GenesSequence[i], firstChild.GenesSequence[i]) = (firstChild.GenesSequence[i], secondChild.GenesSequence[i]);
            }
            return (firstChild,secondChild);
        }
    }
}