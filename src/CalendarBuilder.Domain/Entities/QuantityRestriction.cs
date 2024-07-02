using System.Text.Json;
using CalendarBuilder.Domain.Common;
public class QuantityRestriction : BaseEntity
{
    public Guid CalendarId { get; set; }
    public Calendar Calendar { get; set; }
    public bool IsActive { get; set; }
    public Guid SportId { get; set; }
    public Sport Sport { get; set; }
    public int Quantity { get; set; }

    public QuantityRestriction(){}
    public override T Initialize<T>(JsonElement createModel)
    {
        QuantityRestrictionCreateModel model; 
        try {
            model = JsonSerializer.Deserialize<QuantityRestrictionCreateModel>(createModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        SportId = model!.SportId; 
        IsActive = model!.IsActive; 
        CalendarId = model!.CalendarId; 
        Quantity = model!.Quantity; 
        return (this as T)!;    
    }

    public override T Update<T>(JsonElement updateModel)
    {
        QuantityRestrictionUpdateModel model; 
        try {
            model = JsonSerializer.Deserialize<QuantityRestrictionUpdateModel>(updateModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        SportId = model!.SportId ?? SportId; 
        IsActive = model!.IsActive ?? IsActive; 
        CalendarId = model!.CalendarId ?? CalendarId; 
        Quantity = model!.Quantity ?? Quantity; 
        return (this as T)!;            
    }
}
class QuantityRestrictionCreateModel
{
    public int Quantity { get; set; }
    public Guid SportId { get; set; }
    public bool IsActive { get; set; }
    public Guid CalendarId { get; set; }
} 
class QuantityRestrictionUpdateModel
{
    public int? Quantity { get; set; }
    public Guid? SportId { get; set; }
    public bool? IsActive { get; set; }
    public Guid? CalendarId { get; set; }
} 