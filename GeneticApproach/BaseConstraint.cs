public class BaseConstraint<Base>
{
    private readonly Func<Base, double> _func;
    public string Identifier { get; private set; }

    public BaseConstraint(string identifier, Func<Base, double> func)
    {
        Identifier = identifier;
        _func = func;
    }
    public double Evaluate(Base @base)
    {
        return _func(@base);
    }
}
