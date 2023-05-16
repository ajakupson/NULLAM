using Microsoft.AspNetCore.Mvc;
using NULLAM_RIK.DataModels;
using NULLAM_RIK.DTO;
using NULLAM_RIK.IRepository;
using NULLAM_RIK.Repositorty;
using System.ComponentModel.DataAnnotations;
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

        [Route("event-add")]
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
            EventDTO eventDTO = new EventDTO();

            if (evt != null)
            {
                eventDTO = new EventDTO(evt);
                eventDTO.DateTimeFormatted = evt.DateTime.ToString("dd.MM.yyyy");
                ViewBag.Event = JsonSerializer.Serialize(eventDTO);
                return View();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("/event/participants/add")]
        public IActionResult AddParticipant([FromBody] ParticipantDataModel participant)
        {
            ICollection<ValidationResult> results = null;
            if (!Helpers.ModelValidation.Validate(participant, out results))
            {
                return BadRequest(new { errMsg = "Valed andmed" });
            }

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
            ICollection<ValidationResult> results = null;
            if (!Helpers.ModelValidation.Validate(evt, out results))
            {
                return BadRequest(new { errMsg = "Valed andmed" });
            }

            _eventRepository.AddEvent(evt);
            return Ok();
        }

        [HttpGet]
        [Route("/event/participants/{id}")]
        public IActionResult ParticipantDetails(int id)
        {
            ParticipantDataModel pdm = _participantRepository.GetParticipantById(id);
            if (pdm != null)
            {
                ViewBag.Participant = JsonSerializer.Serialize(pdm);
                return View();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("/event/participants/save")]
        public IActionResult SaveParticipant([FromBody] ParticipantDataModel participant)
        {
            ICollection<ValidationResult> results = null;
            if (!Helpers.ModelValidation.Validate(participant, out results))
            {
                return BadRequest(new { errMsg = "Valed andmed" });
            }

            _participantRepository.SaveParticipant(participant);
            return Ok();
        }
    }
}
