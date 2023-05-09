using Microsoft.AspNetCore.Mvc;
using Lab16.Models;
using Lab16.Models.ViewModels;
using UtilityLibraries;
namespace Lab16.Controllers;
public class HomeController : Controller
{
    IPersonRepository PersonRepository {get; set;}
    
    public HomeController(IPersonRepository personRepository)    
    {
        this.PersonRepository = personRepository;       
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
    [HttpPost]
    public IActionResult Index(ActionResponse actionResponse)
    {     
       
        if(actionResponse.action == "add")
        {
            PersonRepository.Add(
                new Person(actionResponse.first_name, 
                actionResponse.surname, 
                actionResponse.patronymic)
            );            
        }
        else if(actionResponse.action == "delete")
        {
            PersonRepository.Remove(new Person(actionResponse.first_name, 
                actionResponse.surname, 
                actionResponse.patronymic));
        }
        else
        {
            throw new ArgumentException();
        }
        ViewData["Collection"] = PersonRepository.Persons;
        return View();
    }
}