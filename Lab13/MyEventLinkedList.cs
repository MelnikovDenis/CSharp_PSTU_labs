namespace Lab13;
//класс коллекции с событиями 
public class MyEventLinkedList<T> : MyExtendedLinkedList<T>{      
      public delegate void MyLinkedListHandler(MyLinkedListHandlerEventArgs args);
      public event MyLinkedListHandler? CountChanged = null;
      public event MyLinkedListHandler? ReferenceChanged = null;
      public override T this[int index]{
            get{
                 return base[index];
            }
            set{
                  ReferenceChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[3], base[index]));
                  base[index] = value;
            }
      }
      public MyEventLinkedList(string Name) : base(Name){
            
      }
      public MyEventLinkedList(IEnumerable<T> enumerable, string Name) : base(enumerable, Name){
            
      }
      public override void Add(T Data){
            CountChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[0], Data));
            base.Add(Data);
      }
      public override void AddTo(T Data, int index)
      {
            CountChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[0], Data));
            base.AddTo(Data, index);
      }
      public override void Clear(){
            CountChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[2], this));
            base.Clear();
      }   
      public override bool Remove(T item)
      {
            if (Contains(item))
            {
                  CountChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[1], item));
                  if (item is not null)
                  {
                        if (Count != 1)
                        {
                              if (StartNode!.Data!.Equals(item))
                              {
                                    StartNode = StartNode.NextNode;
                              }
                              else
                              {
                                    Node? currentNode = StartNode;
                                    while (!item.Equals(currentNode!.NextNode!.Data))
                                    {
                                          currentNode = currentNode.NextNode;
                                    }
                                    currentNode.NextNode = currentNode.NextNode.NextNode;
                              }
                        }
                        else
                        {
                              StartNode = null;
                        }
                        --Count;
                        return true;
                  }
                  else
                  {
                        if (Count != 1)
                        {
                              if (StartNode!.Data!.Equals(item))
                              {
                                    StartNode = StartNode.NextNode;
                              }
                              else
                              {
                                    Node? currentNode = StartNode;
                                    while (!(currentNode!.NextNode!.Data is null))
                                    {
                                          currentNode = currentNode.NextNode;
                                    }
                                    currentNode.NextNode = currentNode.NextNode.NextNode;
                              }
                        }
                        else
                        {
                              StartNode = null;
                        }
                        --Count;
                        return true;
                  }
            }
            else
            {
                  CountChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[1], null));
                  return false;
            }
      }
      public override bool RemoveFrom(int index)
      {
            if(index >= 0 && index < Count){
                  CountChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[1], this[index]));
                  if (Count != 1)
                  {
                        if(index == 0){
                              StartNode = StartNode!.NextNode;
                        }
                        else{
                              Node? currentNode = StartNode;
                              while(index-- > 0){
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
                  CountChanged?.Invoke(new MyLinkedListHandlerEventArgs(this, Name, MyLinkedListHandlerEventArgs.ChangeTypes[1], null));
                  return false;
            }           
      }    
}