using System.Net.Mime;
using System.Xml.Serialization;

namespace Lab16.Models.SerializationModels;

public class XmlSerializator : ISerializator
{
    public string FileType => "xml";
    public string MIMEType => MediaTypeNames.Application.Xml;
    
    public MemoryStream Serialize<T>(T obj)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        var ms = new MemoryStream();
        xmlSerializer.Serialize(ms, obj);
        return ms;
    }
    public T Deserialize<T>(Stream ms)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));         
        T obj = (T)xmlSerializer.Deserialize(ms);
        return obj;
    }
}