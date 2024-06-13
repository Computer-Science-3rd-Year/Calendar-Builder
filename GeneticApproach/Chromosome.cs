using GeneticApproach.Extensions;
using GeneticSharp;

internal class Chromosome<T> : ChromosomeBase 
    where T : IRandomGenerable<T>
{
    public List<string> ActiveConstraints { get; set; } = new List<string>();
    private readonly int _size;

    public Chromosome(int size) : base (size)
    {
       for (int i = 0; i < Length; i++)
        {
            ReplaceGene(i, GenerateGene(i));
        }
        _size = size;
    }
    public override IChromosome CreateNew()
    {
        return new Chromosome<T>(_size); 
    }

    public override Gene GenerateGene(int geneIndex)
    {
        return new Gene(T.GenerateRandom());
    }
    public void Draw()
    {
        foreach (var item in GetGenes())
        {
            T.Draw((T)item.Value); 
        }
    } 
}
