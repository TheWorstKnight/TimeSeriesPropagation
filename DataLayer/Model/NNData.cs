using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class NNData
    {
        public List<double> TrainData { get; }
        public List<double> TestData { get; }

        public NNData()
        {
            TrainData = new List<double> ();
            TestData = new List<double> ();
        }
    }
}
