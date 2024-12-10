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
        public string Date {  get; set; }

        internal TimeSpan CalculateDuration(string s, string e)
        {
            DateTime startDateTime = DateTime.ParseExact(s, "dd/MM/yyyy HHmm", CultureInfo.CurrentCulture);
            DateTime endDateTime = DateTime.ParseExact(e, "dd/MM/yyyy HHmm", CultureInfo.CurrentCulture);

            return (endDateTime - startDateTime);
        }
    }
}