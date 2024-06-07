internal class Program
{
    private static void Main(string[] args)
    {

        DayConstrain c = new DayConstrain();
        MyCalendar can = new MyCalendar(){
            GenesSequence = new List<Days>(){}
        };
        c.GetConstraintValue(can);
    }
}