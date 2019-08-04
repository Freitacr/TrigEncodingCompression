using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrigEncodingCompression
{
    public static class ErrorFunctions
    {
        public static Func<byte[], List<float>, int, Func<List<byte>, List<float>, byte>, double> SquaredErrors { get { return SSE; } }

        private static double SSE(byte[] dataBlockIn, List<float> parametersIn, int order, Func<List<byte>, List<float>, byte> PredictorFunction)
        {
            List<byte> currentData = new List<byte>();
            for (int i = 0; i < order; i++)
                currentData.Add(dataBlockIn[i]);
            double error = 0;
            for (int i = order; i < dataBlockIn.Length; i++)
            {
                byte expectedValue = dataBlockIn[i];
                byte predictedValue = PredictorFunction(currentData, parametersIn);
                error += Math.Pow(expectedValue - predictedValue, 2);
                for (int j = 1; j < order; j++)
                    currentData[j - 1] = currentData[j];
                currentData[order - 1] = predictedValue;
            }
            return error;
        }
    }
}
