using GeneticApproach.Abstract;
public class DayConstrain : AbstractConstraint<MyCalendar, Days>
{
    public override double GetConstraintValue(MyCalendar chromosome)
    {
        throw new NotImplementedException();
    }
}

public class MyCalendar : AbstractChromosome<Days>
{
    public override double GetFitness(IEnumerable<AbstractConstraint<AbstractChromosome<Days>, Days>> constraints)
    {
        throw new NotImplementedException();
    }

    public override AbstractChromosome<Days> GetRandom()
    {
        throw new NotImplementedException();
    }

    public override void Mutate()
    {
        throw new NotImplementedException();
    }
}

public class Days
{
}