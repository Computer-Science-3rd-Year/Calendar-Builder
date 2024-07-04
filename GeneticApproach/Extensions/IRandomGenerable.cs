namespace GeneticApproach.Extensions
{
    // public interface IRandomGenerable<T> where T : IRandomGenerable<T>
    // {
    //     public abstract static T GenerateRandom();
    //     public abstract static void Draw(T target);
    // }
    public interface IRandomGenerableFactory<T> where T :  class
    {
        public abstract T GenerateRandom();
        public abstract void Draw(T target);
    }
}