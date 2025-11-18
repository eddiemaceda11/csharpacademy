using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coding_logger
{
    public class CodingSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }

        public CodingSession(int id, DateTime startTime,
            DateTime endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Duration = endTime - startTime;
        }
    }
}

//You'll need to create a "CodingSession" class in a separate file.
//It will contain the properties of your coding session:
//Id, StartTime, EndTime, Duration

