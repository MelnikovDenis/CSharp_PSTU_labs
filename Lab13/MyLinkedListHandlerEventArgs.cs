namespace Lab13;
//класс, который хранит данные о том, какое событие произошло
public class MyLinkedListHandlerEventArgs : EventArgs{
      public static readonly string[] ChangeTypes = new string[] {"ADD", "REMOVE", "REMOVEALL", "UPDATE"}; //варианты с типами изменений
      public string CollectionName {get; private set;} //имя коллекции, в которой произошло событие
      public string ChangeType {get; private set;} //тип изменений в коллекции
      public object? ChangedObj {get; private set;} = null; //объект, который подвергся изменениям
      public object Sender {get; private set;} //объект коллекции, вызвавшей событие
      public MyLinkedListHandlerEventArgs(object Sender, string CollectionName, string ChangeType, object? ChangedObj){
            this.Sender = Sender;
            this.CollectionName = CollectionName;
            this.ChangeType = ChangeType;
            this.ChangedObj = ChangedObj;
      }
      public override string ToString(){
            return $"Object: {ChangedObj?.ToString() ?? "NULL"}\tOperation: {ChangeType}\tSourceCollection: {CollectionName}";
      }
}