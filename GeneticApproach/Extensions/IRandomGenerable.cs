namespace GeneticApproach.Extensions
{
    internal interface IRandomGenerable<T> where T : IRandomGenerable<T>
    {
        abstract static T GenerateRandom();
        abstract static void Draw(T target);
    }
}