public interface IGeneticApproachWrapper
{
    Task<IEnumerable<GeneticResults>> Evolution(Guid calendarId); 
}