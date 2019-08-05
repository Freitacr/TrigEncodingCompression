using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace TrigEncodingCompression
{
    public class Program
    {

        private static readonly int DEFAULT_ORDER = 3;

        private static List<float> RandomlyInitializeParameters(int order)
        {
            List<float> ret = new List<float>();
            Random rand = new Random();
            for (int i = 0; i < order; i++)
                ret.Add((float)rand.NextDouble());
            return ret;
        }

        private static byte[] ReadBlockFromFile(int blockSize, Stream streamIn)
        {
            byte[] ret = new byte[blockSize];
            int read = streamIn.Read(ret, 0, blockSize);
            if (read < blockSize)
                throw new EndOfStreamException("Reached end of stream before reading requested number of bytes");
            return ret;
        }

        [STAThread]
        public static void Main()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files (*.*)|*.*";
            var res = dlg.ShowDialog();
            if (res != DialogResult.OK)
                return;
            var parameters = RandomlyInitializeParameters(DEFAULT_ORDER);
            FileStream streamIn = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
            byte[] dataIn = ReadBlockFromFile(1024, streamIn);
            CurveFitter fitter = new CurveFitter(DEFAULT_ORDER);
            fitter.ErrorFunction = ErrorFunctions.SquaredErrors;
            fitter.FitData(dataIn, parameters, 1E-8, PredictorFunctions.PureSinePredictor, 100);
            fitter.DecorrelateData(dataIn, parameters, PredictorFunctions.PureSinePredictor);
            List<int> dataToPass = new List<int>();
            Dictionary<byte, int> countingDict = new Dictionary<byte, int>();
            foreach(byte b in dataIn)
            {
                if (!countingDict.ContainsKey(b))
                    countingDict.Add(b, 0);
                countingDict[b] += 1;
            }
            for (int i = 0; i < 256; i++)
            {
                if (!countingDict.ContainsKey((byte)i))
                {
                    dataToPass.Add(0);
                    continue;
                }
                dataToPass.Add(countingDict[(byte)i]);
            }
            BarCharCheck barChartDisplay = new BarCharCheck();
            barChartDisplay.AddData(dataToPass);
            Application.Run(barChartDisplay);
        }
    }
}
