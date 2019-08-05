using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrigEncodingCompression
{
    public partial class BarCharCheck : Form
    {
        public BarCharCheck()
        {
            InitializeComponent();
        }

        public BindingSource ByteDistributionData;

        public void AddData(List<int> yData)
        {
            var byteDistSeries = ByteViewingChart.Series.Add("Byte Distribution");
            for (int i = 0; i < 256; i++)
                byteDistSeries.Points.AddXY(i, yData[i]);
        }
    }
}
