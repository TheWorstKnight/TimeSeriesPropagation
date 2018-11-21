using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Kernels;
using DataLayer.Interaction;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Series_Propagation.Interfaces;

namespace Time_Series_Propagation
{
    public class TimeSeriesPropagation: ITimeSeriesPropagation
    {
        private readonly NNDataInteraction nnDataInterection;
        public List<List<double>> propagationDataX;
        public List<double> propagationDataY;
        public List<List<double>> testPropagationDataX;
        public List<double> testPropagationDataY;

        public TimeSeriesPropagation()
        {
            this.nnDataInterection = new NNDataInteraction();
            propagationDataX = new List<List<double>>();
            propagationDataY = new List<double>();
            testPropagationDataX = new List<List<double>>();
            testPropagationDataY = new List<double>();
        }

        public IEnumerable<double> GetPrimeData()
        {
            var train = nnDataInterection.GetNNData("train");
            var test = nnDataInterection.GetNNData("test");
            return train.Concat(test);
        }

        public void CreateMatrixForPropagation(string filePath)
        {
                if (!nnDataInterection.ReadNNDataFromFile(filePath)) throw new Exception("Something wrong with file or file directory.");
                nnDataInterection.Normalize();
                List<double> incomingArray = nnDataInterection.GetNNData("train");
                List<double> testIncomingArray = nnDataInterection.GetNNData("test");
                if (incomingArray != null)
                {
                    int counter = 0;
                    for (int i = 4; i < incomingArray.Count; i++)
                    {
                        propagationDataX.Add(new List<double>());
                        for (int j = counter; j < i; j++)
                        {
                            propagationDataX[counter].Add(incomingArray[j]);
                        }
                        counter++;
                        propagationDataY.Add(incomingArray[i]);
                    }

                    counter = 0;
                    for (int i = 4; i < testIncomingArray.Count - 1; i++)
                    {
                        testPropagationDataX.Add(new List<double>());
                        for (int j = counter; j < i; j++)
                        {
                            testPropagationDataX[counter].Add(testIncomingArray[j]);
                        }
                        counter++;
                        testPropagationDataY.Add(testIncomingArray[i]);
                    }
                }

                else throw new Exception("Wrong type of data has been chosen.");  
        }

        public List<double> RegressionSupportVectorMachinesPropagate()
        {
            double[][] propArrayX = new double[propagationDataX.Count][];
            double[][] testPropArrayX = new double[testPropagationDataX.Count][];
            int i = 0;
            foreach (var item in propagationDataX)
            {
                propArrayX[i] = item.ToArray();
                i++;
            }

            i = 0;
            foreach (var item in testPropagationDataX)
            {
                testPropArrayX[i] = item.ToArray();
                i++;
            }

            Accord.Math.Random.Generator.Seed = 0;
            var teacher = new FanChenLinSupportVectorRegression<Gaussian>();
            var svm = teacher.Learn(propArrayX, propagationDataY.ToArray());
            double[] predicted = svm.Score(testPropArrayX);
            double error = new SquareLoss(testPropagationDataY.ToArray()).Loss(predicted);
            for (i = 0; i < predicted.Length; i++)
            {
                predicted[i] *= nnDataInterection.MaxTestValue;
                testPropagationDataY[i] *= nnDataInterection.MaxTestValue;
            }
            error*= nnDataInterection.MaxTestValue;
            nnDataInterection.Denormalize();
            return predicted.ToList();
        }

    }
}
