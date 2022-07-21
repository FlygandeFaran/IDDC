using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDDC
{
    public class MeasurementManager
    {
        private List<measurement> listOfMeasurements;
        private string filename;

        public string Filename
        {
            get { return filename; }
        }

        public List<measurement> ListOfMeasurements
        {
            get { return listOfMeasurements; }
            set { listOfMeasurements = value; }
        }

        public MeasurementManager(string fileName)
        {
            listOfMeasurements = new List<measurement>();
            filename = fileName;
        }
        public override string ToString()
        {
            return filename;
        }
    }
}
