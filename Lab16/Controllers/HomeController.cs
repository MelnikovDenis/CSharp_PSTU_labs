using Microsoft.AspNetCore.Mvc;
using Lab16.Models;
using Lab16.Models.SerializationModels;
using Lab16.Models.ViewModels;
using Lab16.Models.Expetions;
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
    public IActionResult DownloadJson() => DownloadFile(new JsonSerializator());
    [HttpGet]
    public IActionResult DownloadXml() => DownloadFile(new XmlSerializator());
    [HttpGet]
    public IActionResult DownloadBinaryFile() => DownloadFile(new BinarySerializator());

    [HttpGet]
    public IActionResult UploadJson() 
    {
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpGet]
    public IActionResult UploadXml() 
    {
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpGet]
    public IActionResult UploadBinaryFile() 
    {
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpPost]
    public IActionResult UploadJson(IFormFile? uploadedFile)
    {       
        try
        {
            UploadFile(uploadedFile, new JsonSerializator());
            
        }
        catch(Exception ex)
        {
            TempData["Message"] = ex.Message;
            return RedirectToAction("Error");
        } 
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpPost]
    public IActionResult UploadXml(IFormFile? uploadedFile)
    {       
        try
        {
            UploadFile(uploadedFile, new XmlSerializator());
            
        }
        catch(Exception ex)
        {
            TempData["Message"] = ex.Message;
            return RedirectToAction("Error");
        }
        ViewData["Collection"] = PersonRepository.Persons; 
        return View();     
    }
    [HttpPost]
    public IActionResult UploadBinaryFile(IFormFile? uploadedFile)
    {
        try
        {
            UploadFile(uploadedFile, new BinarySerializator());            
        }
        catch(Exception ex)
        {
            TempData["Message"] = ex.Message;
            return RedirectToAction("Error");
        }
        ViewData["Collection"] = PersonRepository.Persons;    
        return View();      
    }
    [HttpGet]
    public IActionResult Error()
    {
        ViewData["Collection"] = PersonRepository.Persons;        
        return View(TempData["Message"]);
    }
    [HttpPost]
    public IActionResult UpdatePersonByIndex(UpdatePersonByIndexModel response)
    {
        try
        {
            PersonRepository.UpdateByIndex(response.person, response.index);
        }
        catch(Exception ex)
        {
            TempData["Message"] = ex.Message;
            return RedirectToAction("Error");
        }       
        ViewData["Collection"] = PersonRepository.Persons;    
        return View();
    }
    [HttpGet]
    public IActionResult UpdatePersonByIndex()
    {      
        ViewData["Collection"] = PersonRepository.Persons;    
        return View();
    }
    [NonAction]
    public FileContentResult DownloadFile(ISerializator serializator)
    {
        var serializationWorker = new SerializationWorker(serializator);
        return serializationWorker.SerializeToFileContent<List<Person>>(PersonRepository.Persons.ToList<Person>(), File);
    }
    [NonAction]
    public void UploadFile(IFormFile? uploadedFile, ISerializator serializator)
    {
        if(uploadedFile is not null)
        {
            using(var stream = uploadedFile.OpenReadStream())
            {
                var serializationWorker = new SerializationWorker(serializator);
                var persons = serializationWorker.Serializator.Deserialize<List<Person>>(stream);
                if(persons is not null && persons.Count() > 0)
                    foreach(var item in persons)
                        PersonRepository.Add(item);
                else 
                    throw new FileDataException();
            }
        }
        else
            throw new FileDataException();
    }
}