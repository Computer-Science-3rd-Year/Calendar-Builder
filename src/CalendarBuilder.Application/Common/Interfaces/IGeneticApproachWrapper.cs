public interface IGeneticApproachWrapper
{
    Task<GeneticResults> Evolution(Guid calendarId); 
}