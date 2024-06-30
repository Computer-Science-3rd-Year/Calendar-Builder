using System.Text.Json;
using CalendarBuilder.Domain.Common;
public class Sport : BaseEntity
{
    public string Name { get; set; }
    private Sport(string name)
    {
        Name = name;
    }

    public Sport()
    {
        Name = "NotInitialized";
    }
    public static Sport Create(string name)
    {
        return new Sport(name);
    }

    public override T Initialize<T>(JsonElement createModel)
    {
        SportCreateModel model; 
        try {
            model = JsonSerializer.Deserialize<SportCreateModel>(createModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        Name = model!.Name; 
        return (this as T)!;    
    }

    public override T Update<T>(JsonElement updateModel)
    {
        SportUpdateModel model; 
        try {
            model = JsonSerializer.Deserialize<SportUpdateModel>(updateModel, new JsonSerializerOptions(){
                PropertyNameCaseInsensitive = true
            }) ?? throw new Exception();
        }catch{
            throw new Exception("Error casting from generic model to "+ GetType().Name+ " create model.");
        }
        Name = model!.Name ?? Name; 
        return (this as T)!;            
    }
}
class SportCreateModel
{
    public required string Name { get; set; }
} 
class SportUpdateModel
{
    public string? Name { get; set; }
} 