using DataLayer.Interfaces;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interaction
{
    public class NNDataInteraction: INNDataInteraction
    {
        private NNData nnData;
        private double maxTestValue=0;
        private double maxTrainValue=0;

        public NNDataInteraction()
        {
            nnData = new NNData();
        }

        public double MaxTrainValue { get { return maxTrainValue; } }
        public double MaxTestValue { get { return maxTestValue; } }

        public bool ReadNNDataFromFile(string fileDirectory)
        {
            List<double> convertedData = new List<double>();
            nnData.TrainData.Clear();
            nnData.TestData.Clear();
            double convertedResult = 0;
            try
            {
                using (StreamReader sr = new StreamReader(fileDirectory))
                {
                    string data = sr.ReadToEnd();
                    char[] charsToTrim = { '\r', '\n' };
                    string[] parsedData = data.Split(new char[] { ',', ';' });

                    foreach (var substring in parsedData)
                    {
                        substring.Trim(charsToTrim);
                        bool success = double.TryParse(substring, out convertedResult);
                        if (!success) return false;
                        convertedData.Add(convertedResult);
                    }

                    int i = 0;
                    while (i < (convertedData.Count * 2) / 3)
                    {
                        nnData.TrainData.Add(convertedData[i]);
                        i++;
                    }
                    while (i < convertedData.Count)
                    {
                        nnData.TestData.Add(convertedData[i]);
                        i++;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Normalize()
        {
            maxTestValue = nnData.TestData.Max();
            for (int i = 0; i < nnData.TestData.Count; i++)
                nnData.TestData[i] /= maxTestValue;

            maxTrainValue = nnData.TrainData.Max();
            for (int i = 0; i < nnData.TrainData.Count; i++)
                nnData.TrainData[i] /= maxTrainValue;
        }

        public void Denormalize()
        {
            if (maxTestValue != 0)
            for (int i = 0; i < nnData.TestData.Count; i++)
                nnData.TestData[i] *= maxTestValue;
            
            if(maxTrainValue!=0)
            for (int i = 0; i < nnData.TrainData.Count; i++)
                nnData.TrainData[i] *= maxTrainValue;
        }

        public List<double> GetNNData(string type)
        {
            switch (type)
            {
                case "train":
                    return nnData.TrainData;
                case "test":
                    return nnData.TestData;
                default:
                    return null;
            }
        }
    }
}
