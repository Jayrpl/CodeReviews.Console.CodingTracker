using System;
using System.Globalization;


namespace coding_tracker
{
    record CodingSession()
    {
        public int Id { get; set; }
        public long Duration { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        internal TimeSpan CalculateDuration(string s, string e)
        {
            DateTime sTime = DateTime.ParseExact(s, "dd/mm/yyyy", CultureInfo.CurrentCulture);
            DateTime eTime = DateTime.ParseExact(e, "dd/mm/yyyy", CultureInfo.CurrentCulture);

            return (eTime - sTime);
        }
    }
}