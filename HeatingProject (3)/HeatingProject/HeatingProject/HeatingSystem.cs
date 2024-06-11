using System;

namespace HeatingProject
{
    public class HeatingSystem
    {
        public string City { get; set; }
        public int BoilerNumber { get; set; }
        public int NumberOfHeatingObjects { get; set; }
        public DateTime StartDate { get; set; }
        public double StartTemperature { get; set; }
        public DateTime EndDate { get; set; }

        public int GetHeatingSeasonDuration()
        {
            return (EndDate - StartDate).Days;
        }
    }
}
