using System.Text;
namespace Lab13;
//класс для журнала логов
class Journal<T>{
      //список с логами
      public List<MyLinkedListHandlerEventArgs> Changes {get; private set;} = new List<MyLinkedListHandlerEventArgs>();
      public Journal(){

      }
      //метод, который будет цепляться к событиям
      public void AddChange(MyLinkedListHandlerEventArgs change){
            Changes.Add(change);
      }
      public override string ToString()
      {
            if(Changes.Count > 0){
                  StringBuilder stringBuilder = new StringBuilder(Changes.Count * 30);
                  stringBuilder.Append("Журнал изменений:\n");
                  int counter = 0;
                  foreach(var change in Changes){
                        stringBuilder.Append(counter++).Append(". ").Append(change).Append('\n');
                  }            
                  return stringBuilder.ToString();
            }
            else{
                  return "Журнал изменений пуст!";
            }
           
      }
}