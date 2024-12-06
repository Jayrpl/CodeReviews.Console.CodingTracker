using System;


namespace coding_tracker
{
    record CodingSession(DateTime startTime, DateTime endTime)
    {
        private int id { get; set; }
        private float duration { get; set; }

        internal TimeSpan CalculateDuration()
        {
            return (startTime - endTime);
        }
    }