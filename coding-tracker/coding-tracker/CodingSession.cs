using System;


namespace coding_tracker
{
    record CodingSession()
    {
        public int id { get; set; }
        public float duration { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        internal TimeSpan CalculateDuration()
        {
            return (startTime - endTime);
        }
    }
}