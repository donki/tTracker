using Microsoft.AspNetCore.Mvc;
using tTrackerWeb.Database;
using tTrackerWeb.Helpers;
using tTrackerWeb.Model;
using tTrackerWeb.Services;

namespace tTrackerWeb.Controllers
{
    [Route("api/timeSegments")]
    [ApiController]
    [TypeFilter(typeof(TokenAuthorizationFilter))]
    public class TimeSegmentController : ControllerBase
    {
        private readonly TimeSegmentService timeSegmentService;

        public TimeSegmentController(SqliteDbContext dbContext)
        {
            timeSegmentService = new TimeSegmentService(dbContext); // Instancia de la API de tramos horarios
        }

        [HttpGet]
        public ActionResult<IEnumerable<TimeSegment>> Get()
        {
            List<TimeSegment> timeSegments = timeSegmentService.GetTimeSegments();
            return Ok(timeSegments);
        }

        [HttpGet("{id}")]
        public ActionResult<TimeSegment> GetTimeSegment(int id)
        {
            List<TimeSegment> timeSegments = timeSegmentService.GetTimeSegments();
            var timeSegment = timeSegments.FirstOrDefault(t => t.Id == id);

            if (timeSegment == null)
            {
                return NotFound();
            }

            return Ok(timeSegment);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TimeSegment timeSegment)
        {
            if (timeSegment == null)
            {
                return BadRequest();
            }

            timeSegmentService.AddTimeSegment(timeSegment);
            return CreatedAtAction(nameof(Get), new { id = timeSegment.Id }, timeSegment);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TimeSegment updateTimeSegment)
        {
            if (updateTimeSegment == null)
            {
                return BadRequest();
            }

            var existingTimeSegment = timeSegmentService.GetTimeSegmentById(id);

            if (existingTimeSegment == null)
            {
                return NotFound();
            }

            updateTimeSegment.Id = id;
            existingTimeSegment.Id = updateTimeSegment.Id;

            timeSegmentService.UpdateTimeSegment(updateTimeSegment);

            return Ok(updateTimeSegment);
        }
    }
}
