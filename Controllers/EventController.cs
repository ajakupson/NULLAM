using Microsoft.AspNetCore.Mvc;
using NULLAM_RIK.DataModels;
using NULLAM_RIK.DTO;
using NULLAM_RIK.IRepository;
using NULLAM_RIK.Repositorty;
using System.Text.Json;

namespace NULLAM_RIK.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly IParticipantRepository _participantRepository;

        public EventController(ILogger<EventController> logger, IEventRepository eventRepository, IParticipantRepository participantRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _participantRepository = participantRepository;
        }

        public IActionResult Index()
        {
            ViewBag.MenuSelection = "add-event";
            return View();
        }

        [HttpGet]
        [Route("/event/{id}/participants")]
        public IActionResult Participants(int id)
        {
            EventDataModel evt = _eventRepository.GetEventWithParticipants(id);
            ViewBag.Event = JsonSerializer.Serialize(evt);
            return View();
        }

        [HttpPost]
        [Route("/event/participants/add")]
        public IActionResult AddParticipant([FromBody] ParticipantDataModel participant)
        {
            int id = _participantRepository.AddParticipant(participant);
            return Ok(new { id = id });
        }

        [HttpPost]
        [Route("/event/participants/remove")]
        public IActionResult RemoveParticipant([FromBody] ParticipantDataModel participant)
        {
            _participantRepository.RemoveParticipant(participant);
            return Ok();
        }

        [HttpPost]
        [Route("/event/{id}/remove")]
        public IActionResult RemoveEvent(int id)
        {
            _eventRepository.RemoveEvent(id);
            return Ok();
        }

        [HttpPost]
        [Route("/event/add")]
        public IActionResult AddEvent([FromBody] EventDataModel evt)
        {
            _eventRepository.AddEvent(evt);
            return Ok();
        }

        public IActionResult ParticipantDetails()
        {
            return View();
        }
    }
}
