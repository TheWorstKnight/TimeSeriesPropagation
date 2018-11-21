using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface INNDataInteraction
    {
        bool ReadNNDataFromFile(string fileDirectory);
        void Normalize();
        void Denormalize();
        List<double> GetNNData(string type);
    }
}
