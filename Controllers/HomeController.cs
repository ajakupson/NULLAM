using Microsoft.AspNetCore.Mvc;
using NULLAM_RIK.DataModels;
using NULLAM_RIK.DTO;
using NULLAM_RIK.IRepository;
using NULLAM_RIK.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace NULLAM_RIK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventRepository _eventRepository;

        public HomeController(ILogger<HomeController> logger, IEventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            ViewBag.MenuSelection = "home";

            List<EventDataModel> eventsList = _eventRepository.GetEvents();
            List<EventDTO> eventsDTOs = new List<EventDTO>();
            eventsList.ForEach(e =>
            {
               EventDTO dto = new EventDTO(e);
                dto.DateTimeFormatted = e.DateTime.ToString("dd.MM.yyyy");
                eventsDTOs.Add(dto);
            });
            ViewBag.EventsList = JsonSerializer.Serialize(eventsDTOs);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}