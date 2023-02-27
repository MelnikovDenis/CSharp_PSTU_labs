using Lab12;
using UtilityLibraries;
namespace Lab12Test
{
	[TestClass]
	public class MyLinkedListTest
	{
		//тест foreach и add, когда в коллекции много элементов
		[TestMethod]
		public void ForeachWithManyElementsTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			int num = 1, sum = 0;
			nums.Add(1);
			nums.Add(2);
			nums.Add(3);
			Assert.AreEqual<int>(3, nums.Count);
			foreach(var item in nums){
				sum += num;           
				Assert.AreEqual<int>(num++, item);
			}
			Assert.AreEqual<int>(6, sum);
		}
		//тест foreach и add, когда в коллекции 1 элемент
		[TestMethod]
		public void ForeachWithOneElementTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			int num = 1;
			nums.Add(1);
			Assert.AreEqual<int>(1, nums.Count);
			foreach(var item in nums){
				Assert.AreEqual<int>(num, item);
			}
		}
		//тест foreach, когда в коллекции 0 элементов
		[TestMethod]
		public void ForeachWithZeroElementTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			Assert.AreEqual<int>(0, nums.Count);
			foreach(var item in nums){
				Assert.Fail("Цикл foreach с 0 элементов запустился (не должен был)");
			}
		}
		//тест Remove() - удаление из середины коллекции со множеством элементов
		[TestMethod]
		public void MiddleRemovetWithManyElementsTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			int num = 1;
			nums.Add(1);
			nums.Add(2);
			nums.Add(4);
			nums.Add(3);
			Assert.IsTrue(nums.Remove(4));
			Assert.AreEqual<int>(3, nums.Count);
			foreach(var item in nums){
				Assert.AreEqual<int>(num++, item);
			}
		}
		//тест Remove() - удаление из начала коллекции со множеством элементов
		[TestMethod]
		public void StartRemovetWithManyElementsTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			int num = 1;
			nums.Add(4);
			nums.Add(1);
			nums.Add(2);        
			nums.Add(3);
			Assert.IsTrue(nums.Remove(4));
			Assert.AreEqual<int>(3, nums.Count);
			foreach(var item in nums){
				Assert.AreEqual<int>(num++, item);
			}
		}
		//тест Remove() - удаление из конца коллекции со множеством элементов
		[TestMethod]
		public void EndRemovetWithManyElementsTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			int num = 1;        
			nums.Add(1);
			nums.Add(2);        
			nums.Add(3);
			nums.Add(4);
			Assert.IsTrue(nums.Remove(4));
			Assert.AreEqual<int>(3, nums.Count);
			foreach(var item in nums){
				Assert.AreEqual<int>(num++, item);
			}
		}
		//тест Remove() - удаление из коллекции с 1 элементом
		[TestMethod]
		public void RemovetWithOneElementTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			int num = 1;        
			nums.Add(4);
			Assert.IsTrue(nums.Remove(4));
			Assert.AreEqual<int>(0, nums.Count);
			foreach(var item in nums){
				Assert.Fail("Цикл foreach с 0 элементов запустился (не должен был)");
			}
			nums.Add(1);
			foreach(var item in nums){
				Assert.AreEqual<int>(num, item);
			}
		}
		//тест Contains() - нахождение элемента
		[TestMethod]
		public void ContainsTest()
		{
			var myLinkedList = UserInterface.GenerateMyLinkedList();
			myLinkedList.Add(new Person("Денис", "Мельников", "Вячеславович"));
			Assert.IsTrue(myLinkedList.Contains(new Person("Денис", "Мельников", "Вячеславович")));
			Assert.IsFalse(myLinkedList.Contains(new Person()));
		}
		//тест Clear() - очищение коллекции
		[TestMethod]
		public void ClearTest()
		{
			var myLinkedList = UserInterface.GenerateMyLinkedList();
			myLinkedList.Clear();
			Assert.AreEqual<int>(0, myLinkedList.Count);
			foreach(var item in myLinkedList){
				Assert.Fail("Цикл foreach с 0 элементов запустился (не должен был)");
			}
		}
		//тест AddTo()
		[TestMethod]
		public void AddToTest()
		{
			MyLinkedList<int> nums = new MyLinkedList<int>();
			int num = 1, sum = 0;
			nums.AddTo(1, 0);
			nums.Add(3);
			nums.Add(5);
			nums.AddTo(2, 1);
			nums.AddTo(4, 3);
			nums.AddTo(6, 5);        
			Assert.AreEqual<int>(6, nums.Count);
			foreach(var item in nums){
				sum += num;           
				Assert.AreEqual<int>(num++, item);
			}
			Assert.AreEqual<int>(21, sum);
		}
	}
}

