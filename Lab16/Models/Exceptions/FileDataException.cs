namespace Lab16.Models.Expetions;
public sealed class FileDataException : Exception
{
      public FileDataException() :
            base($"Неверный формат данных в файле")
      {
            
      }
}