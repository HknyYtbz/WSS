using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorFdApp
{
    public class CSinkDevice
    {
        public string deviceID;
        public string subjectID;
        public List<string> values;
        public CSinkDevice()
        {
            this.deviceID = "None";
            this.subjectID = "None";
            this.values = new List<string>();

        }
        public CSinkDevice(CSinkDevice c)
        {
            this.deviceID = c.deviceID;
            this.subjectID = c.subjectID;
            this.values = new List<string>();
        }
    }
}
