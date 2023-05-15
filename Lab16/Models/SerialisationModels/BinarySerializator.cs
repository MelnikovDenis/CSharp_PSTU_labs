using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Mime;

namespace Lab16.Models.SerializationModels;
#pragma warning disable SYSLIB0011
public class BinarySerializator : ISerializator
{
      public string FileType => "dat";
      public string MIMEType => MediaTypeNames.Application.Octet;
      public BinaryFormatter formatter { get; set; } = new BinaryFormatter();
      public MemoryStream Serialize<T>(T obj)
      {
            var ms = new MemoryStream();
            formatter.Serialize(ms, obj);
            return ms;
      }
      public T Deserialize<T>(Stream ms)
      {
            T obj = (T)formatter.Deserialize(ms);
            return obj;
      }  
}
#pragma warning restore SYSLIB0011