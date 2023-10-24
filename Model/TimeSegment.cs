using Microsoft.AspNetCore.Mvc;

namespace tTrackerWeb.Model
{
    public class TimeSegment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
