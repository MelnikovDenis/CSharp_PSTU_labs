using Microsoft.AspNetCore.Mvc;
using UtilityLibraries;

namespace Lab16.Models.SerializationModels;
public class SerializationWorker
{
    public delegate FileContentResult FileCreator(byte[] fileContents, string contentType, string? fileDownloadName);
    public ISerializator Serializator {get; set;}
    public string FileName { get; set; } = "persons";
    public SerializationWorker(ISerializator Serializator)
    {
        this.Serializator = Serializator;
    }
    public FileContentResult SerializeToFileContent<T>(T obj, FileCreator fileCreator)
    {
        MemoryStream ms = Serializator.Serialize<T>(obj);
        var binary = ms.ToArray();
        ms.Close();
        return fileCreator(binary, Serializator.MIMEType, $"{FileName}.{Serializator.FileType}");
    }
}