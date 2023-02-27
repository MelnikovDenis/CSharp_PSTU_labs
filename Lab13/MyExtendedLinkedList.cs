using Lab12;
namespace Lab13;
//класс с новыми методами, унаследованный из класса из 12 лабы (так нужно по условию)
public class MyExtendedLinkedList<T> : MyLinkedList<T>{
      public string Name {get; set;}
      public virtual T this[int index]{
            get{
                  if(index >= 0 || index < Count){
                        Node? currentNode = StartNode;
                        while(--index > 0){
                              currentNode = currentNode!.NextNode;
                        }
                        return currentNode!.Data;
                  }
                  else{
                        throw new ArgumentOutOfRangeException("Индекс выходит за границы списка");
                  }
            }
            set{
                  if(index >= 0 || index < Count){
                        Node? currentNode = StartNode;
                        while(index-- > 0){
                              currentNode = currentNode!.NextNode;
                        }
                        currentNode!.Data = value;
                  }
                  else{
                        throw new ArgumentOutOfRangeException("Индекс выходит за границы списка");
                  }
            }
      }
      public MyExtendedLinkedList(string Name){
            this.Name = Name;
      }
      public virtual bool RemoveFrom(int index){
            if(index >= 0 || index < Count){
                  if (Count != 1)
                  {
                        if(index == 0){
                              StartNode = StartNode!.NextNode;
                        }
                        else{
                              Node? currentNode = StartNode;
                              while(--index > 0){
                                    currentNode = currentNode!.NextNode;
                              }
                              currentNode!.NextNode = currentNode!.NextNode!.NextNode;
                        }                       
                  }
                  else
                  {
                        StartNode = null;
                  }
                  --Count;
                  return true;
            }
            else{
                 return false;
            }
      }
}