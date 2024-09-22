using System.Text.Json;
using System.Text.Json.Serialization;
using CalendarBuilder.Domain.Common;
public class CalendarDay : BaseEntity 
{
    [JsonIgnore]
    public virtual Calendar Calendar { get; set; }
    public DateTime Date { get; set; }
    public virtual Guid CalendarId { get; set; }
    public Guid? MorningSessionSportId { get; set; }
    public Sport? MorningSessionSport { get; set; }
    public Guid? AfterNoonSessionSportId { get; set; }
    public Sport? AfterNoonSessionSport { get; set; }

    public Guid? FullSessionSportId => GetSportIdValue(); 
    private Guid? GetSportIdValue(){
        return MorningSessionSportId == AfterNoonSessionSportId? MorningSessionSportId : null; 
    }
    public Sport? FullSessionSport => GetSportValue(); 
    private Sport? GetSportValue(){
        return FullSessionSportId == null ? null: MorningSessionSport; 
    }
    public CalendarDay()
    {
    }
    public override T Initialize<T>(JsonElement createModel)
    {
        CalendarDayCreateModel model; 
        try {
            model = JsonSerializer.Deserialize<CalendarDayCreateModel>(createModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        return (this as T)!;    
    }

    public override T Update<T>(JsonElement updateModel)
    {
        CalendarDayUpdateModel model; 
        try {
            model = JsonSerializer.Deserialize<CalendarDayUpdateModel>(updateModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        MorningSessionSportId = model!.MorningSessionSportId ?? MorningSessionSportId;  
        AfterNoonSessionSportId = model!.AfterNoonSessionSportId ?? AfterNoonSessionSportId;  
        return (this as T)!;            
    }
    public IEnumerable<Guid?> SportIds()
    {
        yield return MorningSessionSportId; 
        
        yield return AfterNoonSessionSportId;
    }

    public void SetMorning(Guid? id){
        this.MorningSessionSport = null; 
        this.MorningSessionSportId = (id != null && id!=Guid.Empty)? id : null;  
    }
    
    public void SetAfternoon(Guid? id){
        this.AfterNoonSessionSport = null; 
        this.AfterNoonSessionSportId = (id != null && id!=Guid.Empty)? id : null;  
    }
}
class CalendarDayCreateModel
{
} 
class CalendarDayUpdateModel
{
    public Guid? MorningSessionSportId { get; set; }
    public Guid? AfterNoonSessionSportId { get; set; }
} 