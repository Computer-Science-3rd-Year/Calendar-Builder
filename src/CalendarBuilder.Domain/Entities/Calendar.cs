using System.Text.Json;
using System.Text.Json.Serialization;
using CalendarBuilder.Domain.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class Calendar : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<CalendarDay> CalendarDays { get; set; } = new List<CalendarDay>();
    public CalendarStatus Status { get; set; }
    public Calendar()
    {
    }
    public override T Initialize<T>(JsonElement createModel)
    {
        CalendarCreateModel model; 
        try {
            model = JsonSerializer.Deserialize<CalendarCreateModel>(createModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }

        CalendarDays = new List<CalendarDay>();
        for (DateTime date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
        {
            CalendarDays.Add(new CalendarDay
            {
                Id = Guid.NewGuid(),
            });
        }
        
        StartDate = model!.StartDate;
        EndDate = model!.EndDate;
        Status = CalendarStatus.Initial;
        return (this as T)!;    
    }

    public override T Update<T>(JsonElement updateModel)
    {
        CalendarUpdateModel model; 
        try {
            model = JsonSerializer.Deserialize<CalendarUpdateModel>(updateModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " update model.");
        }
        Status = model!.Status ?? Status;
        return (this as T)!;            
    }
    public IEnumerable<Guid?> SportIds()
    {
        foreach (var day in CalendarDays)
        {
            foreach (var sport in day.SportIds())
            {
                yield return sport;                 
            }
        }
    }
    public IEnumerable<Guid?> SportIdsPartialSessions()
    {
        foreach (var day in CalendarDays)
        {
            yield return day.MorningSessionSportId; 
            yield return day.AfterNoonSessionSportId; 
        }
    }
    public IEnumerable<Guid?> SportIdsFullSession()
    {
        foreach (var day in CalendarDays)
        {
            yield return day.FullSessionSportId; 
        }
    }
}

class CalendarCreateModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    // public List<CalendarDay> CalendarDays { get; set; } = new List<CalendarDay>();
} 

class CalendarUpdateModel
{
    public CalendarStatus? Status { get; set; }
    // Fields for updating Calendar can be added here
}
