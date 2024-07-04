using GeneticApproach.Extensions;
using GeneticSharp;

namespace GeneticApproach.Domain
{
    public class Chromosome<T> : ChromosomeBase where T : class
    {
        public List<object> Genes => GetGenes().Select(x => x.Value).ToList(); 
        public List<string> ActiveConstraints { get; set; } = new List<string>();
        private readonly int _size;
        private readonly IRandomGenerableFactory<T> _randomFactory;

        public Chromosome(List<T> initialValues, IRandomGenerableFactory<T> randomFactory) : base(initialValues.Count)
        {
            for (int i = 0; i < initialValues.Count; i++)
            {
                ReplaceGene(i, new Gene(initialValues[i]));
            }
            _size = initialValues.Count;
            _randomFactory = randomFactory;
        } 
        public Chromosome(int size, IRandomGenerableFactory<T> randomFactory) : base(size)
        {
            _size = size;
            _randomFactory = randomFactory;
            for (int i = 0; i < Length; i++)
            {
                ReplaceGene(i, GenerateGene(i));
            }
        }
        public override IChromosome CreateNew()
        {
            return new Chromosome<T>(_size, _randomFactory);
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return  new Gene(_randomFactory.GenerateRandom());
        }
        public void Draw()
        {
            foreach (var item in GetGenes())
            {
                _randomFactory.Draw((T)item.Value);
            }
        }
    }
}