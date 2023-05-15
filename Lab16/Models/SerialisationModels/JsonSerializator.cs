using System.Text;
using System.Net.Mime;
using Newtonsoft.Json;

namespace Lab16.Models.SerializationModels;

public class JsonSerializator : ISerializator
{
    public string FileType => "json";
    public string MIMEType => MediaTypeNames.Application.Json;
    public JsonSerializerSettings settings { get; set; } = new JsonSerializerSettings(){
        TypeNameHandling = TypeNameHandling.All,
        Formatting = Formatting.Indented
    };
    public MemoryStream Serialize<T>(T obj)
    {
        var ms = new MemoryStream();
        var buffer = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(obj, settings));
        ms.Write(buffer, 0, buffer.Count());
        return ms;
    }
    public T Deserialize<T>(Stream ms)
    {
        T obj;        
        using(var sr = new StreamReader(ms, Encoding.Unicode))
        {
            obj = JsonConvert.DeserializeObject<T>(sr.ReadToEnd(), settings);
        }
        return obj;
    }    
}