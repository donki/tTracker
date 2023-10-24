using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using tTrackerWeb.Database;
using tTrackerWeb.Model;

namespace tTrackerWeb.Services
{
    public class TimeSegmentService 
    {
        private SqliteDbContext dbContext;
        private static object timeSegmentsLock = new object();

        public TimeSegmentService(SqliteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<TimeSegment> GetTimeSegments()
        {
            lock (timeSegmentsLock)
            {
                return this.dbContext.TimeSegments.ToList();
            }
        }

        public void AddTimeSegment(TimeSegment newTimeSegment)
        {
            lock (timeSegmentsLock)
            {

                newTimeSegment.Id = dbContext.TimeSegments.Count() + 1;
                dbContext.TimeSegments.Add(newTimeSegment);
                // Guardamos los cambios en la base de datos.
                dbContext.SaveChanges();

            }
        }

        public TimeSegment GetTimeSegmentById(int id)
        {
            lock (timeSegmentsLock)
            {
                var timeSegment = dbContext.TimeSegments.FirstOrDefault(u => u.Id == id);

                return timeSegment;
            }
        }

        public void UpdateTimeSegment(TimeSegment updateTimeSegment)
        {
            lock (timeSegmentsLock)
            {
                var existingTimeSegment = dbContext.TimeSegments.FirstOrDefault(u => u.Id == updateTimeSegment.Id);

                if (existingTimeSegment != null)
                {
                    existingTimeSegment.StartTime = updateTimeSegment.StartTime;
                    existingTimeSegment.EndTime = updateTimeSegment.EndTime;

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
