using DataLayer.Interaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Time_Series_Propagation.Interfaces;

namespace Time_Series_Propagation
{
    public partial class Form1 : Form
    {
        private readonly TimeSeriesPropagation tmPropagation;
        public Form1()
        {
            InitializeComponent();
            this.tmPropagation = new TimeSeriesPropagation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PropagationResults.Series.Clear();
                    tmPropagation.CreateMatrixForPropagation(openFileDialog.FileName);
                    var propagatedResult = tmPropagation.RegressionSupportVectorMachinesPropagate();
                    Series s1 = new Series
                    {
                        Name = "Incoming data",
                        Color = System.Drawing.Color.Blue,
                        IsXValueIndexed = true,
                        ChartType = SeriesChartType.Line
                    };
                    Series s2 = new Series
                    {
                        Name = "Propagated data",
                        Color = System.Drawing.Color.Red,
                        IsXValueIndexed = false,
                        ChartType = SeriesChartType.Line
                    };
                    var primeData = tmPropagation.GetPrimeData();
                    int i = 1;
                    foreach (var item in primeData)
                    {
                        s1.Points.AddXY(i, item);
                        i++;
                    }
                    i = 1;
                    while (i <= (((primeData.Count() * 2) / 3) + 5))
                    {
                        s2.Points.AddXY(i, 1);
                        i++;
                    }
                    foreach (var item in propagatedResult)
                    {
                        s2.Points.AddXY(i, item);
                        i++;
                    }
                    PropagationResults.Series.Add(s1);
                    PropagationResults.Series.Add(s2);
                    PropagationResults.Invalidate();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK); }
            }
            
        }
    }
}
