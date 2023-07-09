using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            revenueBindingSource.DataSource= new List<Revenue>();
            cartesianChart2.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title="Month",
                Labels = new[] {"Jan","Feb","Mar","May","Jun","Jui","Aug","Sep","Oct","Nov","Dec"}
            });
            cartesianChart2.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Revenue",
                LabelFormatter = value => value.ToString("C")
            });
            cartesianChart2.LegendLocation = LiveCharts.LegendLocation.Right;


        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            cartesianChart2.Series.Clear();
            SeriesCollection series= new SeriesCollection();
            var years = (from x in revenueBindingSource.DataSource as List<Revenue>
                         select new {Year1 = x.Year}).Distinct();
            foreach(var year in years)
            {
                List<double> values = new List<double>();
                for(int month = 1; month < 13; month++)
                {
                    double value = 0;
                    var data = from x in revenueBindingSource.DataSource as List<Revenue>
                               where x.Year.Equals(year.Year1) && x.Month.Equals(month)
                               orderby x.Month ascending
                               select new {x.Value, x.Month};
                    if (data.SingleOrDefault() != null)
                        value = data.SingleOrDefault().Value; 
                    values.Add(value);
                }
                series.Add(new LineSeries(){ Title =year.Year1.ToString(), Values = new ChartValues<double>(values) });
            }
            cartesianChart2.Series= series;
        }
    }
}














