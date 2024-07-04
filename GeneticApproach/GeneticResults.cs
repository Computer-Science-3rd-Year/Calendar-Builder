using System.Security.Principal;
using GeneticSharp;

public class GeneticResults
{
    public Gene[] Solution;
    public List<string> ActiveConstraints { get; set; } = new List<string>();
}