using System.Collections;
namespace Lab12;
[Serializable]
public class MyLinkedList<T> : IEnumerable<T>, ICollection<T>, ICloneable
{
      //класс перечислителя (энумератор)
      protected class MyLinkedListEnumerator : IEnumerator<T>
      {
            protected Node? currentNode { get; set; } = null; //текущий узел (будет двигаться)
            protected Node? startNode { get; set; } = null; //стартовый узел             
            public MyLinkedListEnumerator(Node? startNode)
            {
                  this.startNode = startNode;
            }
            //метод передвижения текущего узла
            public bool MoveNext()
            {
                  if (startNode is null) return false;
                  if (currentNode is null)
                  {
                        currentNode = startNode;
                        return true;
                  }
                  else
                  {
                        if (currentNode?.NextNode is null)
                        {
                              Reset();
                              return false;
                        }
                        else
                        {
                              currentNode = currentNode.NextNode;
                              return true;

                        }
                  }
            }
            //свойство для получения данных из текущего узла в виде T
            public T Current
            {
                  get
                  {
                        return currentNode!.Data;
                  }
            }
            //явно реализованное свойство для получения данных из текущего узла в виде object?
            object? IEnumerator.Current => Current;
            //сброс состояния перечислителя
            public void Reset()
            {
                  currentNode = null;
            }
            //метод для удаления неуправляемых ресурсов перечислителя (их нет, так что метод пустой)
            public void Dispose() { }
      }
      [Serializable]
      //класс узлов связного списка
      protected class Node
      {
            public T Data { get; set; } //поле для хранения данных
            public Node? NextNode { get; set; } = null; //адресное поле
            public Node(T Data)
            {
                  this.Data = Data;
            }
            public Node(T Data, Node? NextNode)
            {
                  this.Data = Data;
                  this.NextNode = NextNode;
            }
      }

      protected Node? StartNode { get; set; } = null; //стартовый узел
      public int Count { get; protected set; } = 0; //кол-во узлов
      public bool IsReadOnly { get; } = false;
      public MyLinkedList()
      {
      }
      public MyLinkedList(IEnumerable<T> enumerable)
      {
            foreach(var item in enumerable)
            {
                  Add(item);
            }
      }
      public object Clone()
      {
            var clone = new MyLinkedList<T>();
            if(Count > 0)
            {
                  if(this.First() is ICloneable)
                  {
                        foreach(var item in this)
                        {
                              if(item is not null)
                                    clone.Add((T)((ICloneable)item).Clone());
                              else
                                    clone.Add(item);
                        }
                  }
                  else
                  {
                        foreach(var item in this)
                        {
                              clone.Add(item);
                        }
                  }
            }           
            return clone;
      }
      public virtual void Add(T Data)
      {
            Node? newNode = new Node(Data);
            if (StartNode is null)
            {
                  StartNode = newNode;
            }
            else
            {
                  Node? currentNode = StartNode;
                  while (currentNode?.NextNode is not null)
                  {
                        currentNode = currentNode.NextNode;
                  }
                  currentNode!.NextNode = newNode;
            }
            ++Count;
      }
      public virtual void Clear()
      {
            Count = 0;
            StartNode = null;
      }
      public virtual bool Contains(T item)
      {
            if (item is null)
            {
                  foreach (var obj in this)
                  {
                        if (obj is null)
                        {
                              return true;
                        }
                  }
            }
            else
            {
                  foreach (var obj in this)
                  {
                        if (item!.Equals(obj))
                        {
                              return true;
                        }
                  }
            }
            return false;
      }
      public virtual bool Remove(T item)
      {
            if (Contains(item))
            {
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
                              if (StartNode!.Data is null)
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
                  return false;
            }
      }
      //поверхностное копирование в массив
      public virtual void CopyTo(T[] array, int index)
      {
            if (array.Length < index + Count)
                  throw new ArgumentOutOfRangeException("В целевом массиве недостаточно места для вмещения всех элементов связного списка");
            foreach (var item in this)
            {
                  array[index++] = item;
            }
      }
      //метод для добавления данных на указанный индекс(не метод ICollection<T>)
      public virtual void AddTo(T Data, int index)
      {
            if (index < 0 || index > Count)
                  throw new ArgumentOutOfRangeException("Значение индекса должно быть в диапозоне от 0 до Count");
            if (index == 0)
            {
                  StartNode = new Node(Data, StartNode);
                  ++Count;
            }
            else if (index == Count)
            {
                  Add(Data);
            }
            else
            {
                  Node? currentNode = StartNode;
                  while (index > 1)
                  {
                        currentNode = currentNode?.NextNode;
                        --index;
                  }
                  Node? newNode = new Node(Data, currentNode?.NextNode);
                  currentNode!.NextNode = newNode;
                  ++Count;
            }
      }
      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
            return (IEnumerator<T>)new MyLinkedListEnumerator(StartNode);
      }
      IEnumerator IEnumerable.GetEnumerator()
      {
            return (IEnumerator)new MyLinkedListEnumerator(StartNode);
      }
}