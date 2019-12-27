using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorFdApp
{
    public class CSensor
    {
        public string SensorLocation;
        public string SensorType;
        public string SubjectID;
        public string SensorID;

        public CSensor()
        {
            this.SensorLocation = "No Location";
            this.SensorID = "None";
        }
        public CSensor(string l, string id, string sid, string stype)
        {
            this.SensorLocation = l;
            this.SensorID = id;
            this.SubjectID = sid;
            this.SensorType = stype;
        }
        public CSensor(CSensor c)
        {
            this.SensorLocation = c.SensorLocation;
            this.SensorID = c.SensorID;
            this.SubjectID = c.SubjectID;
            this.SensorType = c.SensorType;
        }
    }
}
