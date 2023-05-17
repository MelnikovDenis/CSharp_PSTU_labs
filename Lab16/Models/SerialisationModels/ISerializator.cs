namespace Lab16.Models.SerializationModels;

public interface ISerializator
{
    public string FileType { get; }
    public string MIMEType { get; }
    public MemoryStream Serialize<T>(T obj);
    public T Deserialize<T>(Stream stream);
}