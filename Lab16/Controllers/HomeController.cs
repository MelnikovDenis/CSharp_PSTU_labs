using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Lab16.Models;
using Lab16.Models.ViewModels;
using UtilityLibraries;
using Lab12;
namespace Lab16.Controllers;
public class HomeController : Controller
{
    IPersonRepository personRepository {get; set;}
    public HomeController(IPersonRepository personRepository)    
    {
        this.personRepository = personRepository;       
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Collection"] = personRepository.Persons;
        return View();
    }
    [HttpPost]
    public IActionResult Index(ActionResponse actionResponse)
    {     
       
        if(actionResponse.action == "add")
        {
            personRepository.Add(
                new Person(actionResponse.first_name, 
                actionResponse.surname, 
                actionResponse.patronymic)
            );
            SavePersonsSW();
        }
        else if(actionResponse.action == "delete")
        {
            personRepository.Remove(new Person(actionResponse.first_name, 
                actionResponse.surname, 
                actionResponse.patronymic));
            SavePersonsSW();
        }
        else
        {
            throw new ArgumentException();
        }
        ViewData["Collection"] = personRepository.Persons;
        return View();
    }
    private void SavePersonsSW()
    {
        using (var sw = new StreamWriter("wwwroot/files/persons.json", false, Encoding.Unicode))
        {
            sw.WriteLine(JsonConvert.SerializeObject(personRepository.Persons));
        }
    }
}