using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace CsvReaderDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "spacex_launches.csv")))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<RocketLaunchClassMap>();
                    var records = csvReader.GetRecords<RocketLaunch>().ToList();                    
                }
            }
        }
    }

    public class RocketLaunchClassMap : ClassMap<RocketLaunch>
    {
        public RocketLaunchClassMap()
        {
            Map(m => m.FlightNumber).Name("flight_number");
            Map(m => m.MissionName).Name("name");
            Map(m => m.LaunchDate).Name("launch_date");
            Map(m => m.Succeeded).Name("success");
            Map(m => m.DidLand).Name("booster_recovered");
        }
    }

    public class RocketLaunch
    {  
        [Index(0)] 
        public int FlightNumber { get; set; }
        [Index(1)] 
        public string MissionName { get; set; }
        [Index(2)] 
        public DateTime LaunchDate { get; set; }
        [Index(4)] 
        public bool Succeeded { get; set; }
        [Index(3)] 
        public bool DidLand { get; set; }
    }

    

    public class SampleClassMap : ClassMap<Sample>
    {
        public SampleClassMap()
        {
            Map(m => m.ShouldBeColumn1).Index(1);
            Map(m => m.ShouldBeColumn2).Index(0);
        }
    }

    public class Sample
    {
        public string ShouldBeColumn1 { get; set; }
        public string ShouldBeColumn2 { get; set; }
    }
}
