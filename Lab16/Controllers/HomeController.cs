using Microsoft.AspNetCore.Mvc;
using Lab16.Models;
using Lab16.Models.SerializationModels;
using UtilityLibraries;
using Lab12;
namespace Lab16.Controllers;
public class HomeController : Controller
{
    IPersonRepository PersonRepository {get; set;}
    
    public HomeController(IPersonRepository personRepository)    
    {
        this.PersonRepository = personRepository;       
    }

    [HttpGet]
    public IActionResult AddPerson()
    {
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpPost]
    public IActionResult AddPerson(Person person)
    {     
       
        PersonRepository.Add(person); 
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpGet]
    public IActionResult RemoveByValue()
    {
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpPost]
    public IActionResult RemoveByValue(Person person)
    {     
       
        PersonRepository.Remove(person); 
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpGet]
    public IActionResult FullNameSort()
    {
        PersonRepository.Persons = PersonRepository.Persons.OrderBy<Person, string>(p => p.surname + p.first_name + p.patronymic);
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpGet]
    public IActionResult GetAdministrators()
    {
        ViewData["Collection"] = from person in PersonRepository.Persons where person is Administrator select person;
        return View();
    }
    [HttpGet]
    public IActionResult GetEngineers()
    {
        ViewData["Collection"] = from person in PersonRepository.Persons where person is Engineer select person;
        return View();
    }
    [HttpGet]
    public IActionResult GenerateNewList()
    {
        PersonRepository.Persons = UserInterface.GenerateMyLinkedList(); 
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpGet]
    public IActionResult DownloadJson()
    {
        var serializationWorker = new SerializationWorker(new JsonSerializator());
        return serializationWorker.SerializeToFileContent<List<Person>>(PersonRepository.Persons.ToList<Person>(), File);
    }
    [HttpGet]
    public IActionResult DownloadXml()
    {
        var serializationWorker = new SerializationWorker(new XmlSerializator());
        return serializationWorker.SerializeToFileContent<List<Person>>(PersonRepository.Persons.ToList<Person>(), File);
    }
    [HttpGet]
    public IActionResult DownloadBinaryFile()
    {
        var serializationWorker = new SerializationWorker(new BinarySerializator());
        return serializationWorker.SerializeToFileContent<List<Person>>(PersonRepository.Persons.ToList<Person>(), File);
    }
}