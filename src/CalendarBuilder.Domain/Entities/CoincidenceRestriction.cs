using System.Text.Json;
using CalendarBuilder.Domain.Common;
public class CoincidenceRestriction : BaseEntity
{
    public Guid CalendarId { get; set; }
    public Calendar Calendar { get; set; }
    public int SessionsGap { get; set; }
    public bool IsActive { get; set; }
    public Guid FirstSportId { get; set; }
    public Sport FirstSport { get; set; }
    public Guid SecondSportId { get; set; }
    public Sport SecondSport { get; set; }
    public CoincidenceRestriction(){}
    public override T Initialize<T>(JsonElement createModel)
    {
        CoincidenceRestrictionCreateModel model; 
        try {
            model = JsonSerializer.Deserialize<CoincidenceRestrictionCreateModel>(createModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        FirstSportId = model!.FirstSportId; 
        SecondSportId = model!.SecondSportId; 
        SessionsGap = model!.SessionsGap; 
        IsActive = model!.IsActive; 
        CalendarId = model!.CalendarId; 
        return (this as T)!;    
    }

    public override T Update<T>(JsonElement updateModel)
    {
        CoincidenceRestrictionUpdateModel model; 
        try {
            model = JsonSerializer.Deserialize<CoincidenceRestrictionUpdateModel>(updateModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        FirstSportId = model!.FirstSportId ?? FirstSportId; 
        SecondSportId = model!.SecondSportId ?? SecondSportId; 
        SessionsGap = model!.SessionsGap ?? SessionsGap; 
        IsActive = model!.IsActive ?? IsActive; 
        CalendarId = model!.CalendarId ?? CalendarId; 
        return (this as T)!;            
    }
}
class CoincidenceRestrictionCreateModel
{
    public int SessionsGap { get; set; }
    public Guid FirstSportId { get; set; }
    public Guid SecondSportId { get; set; }
    public bool IsActive { get; set; }
    public Guid CalendarId { get; set; }
} 
class CoincidenceRestrictionUpdateModel
{
    public int? SessionsGap { get; set; }
    public Guid? FirstSportId { get; set; }
    public Guid? SecondSportId { get; set; }
    public bool? IsActive { get; set; }
    public Guid? CalendarId { get; set; }
} 