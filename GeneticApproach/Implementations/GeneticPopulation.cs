using GeneticApproach.Abstract;
using GeneticApproach.Interfaces;

namespace GeneticApproach.Implementations
{
    public class GeneticPopulation<Gene> : AbstractGeneticPopulation<Gene,Chromosome<Gene>> where Gene : IRandomGenerable<Gene>
    {
        private readonly IGeneGenerator<Gene> _geneGenerator;
        private readonly IEnumerable<AbstractConstraint<Chromosome<Gene>, Gene>> _constraints;
        public override List<Chromosome<Gene>> Chromosomes { get; set; }
        public GeneticPopulation(IGeneGenerator<Gene> geneGenerator,  IEnumerable<AbstractConstraint<Chromosome<Gene>,Gene>> constraints)
        {
            Chromosomes = [];
            _geneGenerator = geneGenerator;
            _constraints = constraints;
        }
        public override AbstractGeneticPopulation<Gene,Chromosome<Gene>> GetRandomPopulation(int populationSize)
        {   
            for (int i = 0; i < populationSize; i++)
            {
                Chromosomes.Add(new Chromosome<Gene>(10, _geneGenerator, _constraints)); //TODO: set the right number  
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
        public override void Evolve()
        {
            var rand = new Random();
            var newGeneration = new List<Chromosome<Gene>>();
            
            // Selection: Tournament Selection
            int tournamentSize = 5 < Chromosomes.Count? 5 : Chromosomes.Count-1;
            for (int i = 0; i < Chromosomes.Count; i++)
            {
                var tournament = new List<Chromosome<Gene>>();
                for (int j = 0; j < tournamentSize; j++)
                {
                    tournament.Add(Chromosomes[rand.Next(Chromosomes.Count)]);
                }
                var bestInTournament = tournament.OrderBy(c => c.GetFitness()).First();
            }

            // Crossover: Apply crossover on the selected population
            for (int i = 0; i < newGeneration.Count - 1; i += 2)
            {
                var firstChild = newGeneration[i];
                var secondChild = newGeneration[i + 1];
                (firstChild, secondChild) = TwoPointCrossOver(firstChild, secondChild);
                newGeneration[i] = firstChild;
                newGeneration[i + 1] = secondChild;
            }

            // Mutation: Apply mutation on the new generation
            double mutationRate = 0.01;// Mutation rate 
            foreach (var chromosome in newGeneration)
            {
                if (rand.NextDouble() < mutationRate)
                {
                    int geneIndex = rand.Next(chromosome.GenesSequence.Count);
                    chromosome.GenesSequence[geneIndex] = _geneGenerator.RandomGenes().First();
                }
            }
            // Replacement: Replace old population with new generation
            Chromosomes = newGeneration;
        }
    }
}