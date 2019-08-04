using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrigEncodingCompression
{
    public static class PredictorFunctions
    {
        public static Func<List<byte>, List<float>, byte> PureSinePredictor { get { return PSP; } }
        public static Func<List<byte>, List<float>, byte> AlternatingSineCosinePredictor { get { return ASCP; } }


        private static byte PSP (List<byte> dataIn, List<float> parametersIn)
        {
            byte ret = 0;
            for (int i = 0; i < dataIn.Count; i++)
            {
                double convertedByte = dataIn[i] * Math.PI / 255;
                ret += (byte)(Math.Floor(Math.Sin(convertedByte) * parametersIn[i] * 255));
            }
            return ret;
        }

        private static byte ASCP (List<byte> dataIn, List<float> parametersIn)
        {
            byte ret = 0;
            for (int i = 0; i < dataIn.Count; i++)
            {
                double convertedByte = dataIn[i] * Math.PI / 255;
                if (i%2 == 1)
                {
                    ret += (byte)(Math.Floor(Math.Sin(convertedByte) * parametersIn[i] * 255));
                } else
                {
                    ret += (byte)(Math.Floor(Math.Cos(convertedByte) * parametersIn[i] * 255));
                }
            }
            return ret;
        }
    }
}
