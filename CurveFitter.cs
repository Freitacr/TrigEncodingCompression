using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrigEncodingCompression
{
    public class CurveFitter
    {

        private static readonly double DEFAULT_LEARNING_RATE = 1E-2;

        public Func<byte[], List<float>, int, Func<List<byte>, List<float>, byte>, double> ErrorFunction { private get; set; }
        private List<bool> ParameterMovementDirections;
        private double LearningRate;
        private int Order;


        public CurveFitter(int order)
        {
            Order = order;
            LearningRate = DEFAULT_LEARNING_RATE;
            ParameterMovementDirections = new List<bool>();
            for (int i = 0; i < order; i++)
                ParameterMovementDirections.Add(true);
        }

        /**
         * <summary>Attempts to fit the parameter's given in initialParameters to the data given by dataBlock</summary>
         * <param name="maxEpochs">The maximum number of epochs fitting will be attempted for. Default=int.MaxValue</param>
         * <param name="minimumErrorChange">The minimum error change that must be observed for fitting to continue. Must be greater than 0</param>
         */
        public void FitData(byte[] dataBlock, List<float> initialParameters, double minimumErrorChange, Func<List<byte>, List<float>, byte> predictionFunction, int maxEpochs = int.MaxValue)
        {
            if (ErrorFunction == null)
                throw new ArgumentNullException("ErrorFunction");
            int currEpochs = 0;
            double currentError, previousError = double.MaxValue;
            bool wasLastZero = false;
            List<float> currentParameters = new List<float>(initialParameters);
            List<double> parameterErrors = new List<double>();
            for (int i = 0; i < initialParameters.Count; i++)
                parameterErrors.Add(0);
            currentError = ErrorFunction(dataBlock, currentParameters, Order, predictionFunction);
            while (currEpochs < maxEpochs)
            {
                Console.WriteLine("On Epoch " + currEpochs + ", error was: " + currentError);
                double maxErrorChange = 0;
                for (int i = 0; i < currentParameters.Count; i++)
                {
                    float savedParameter = currentParameters[i];
                    currentParameters[i] = (float)(ParameterMovementDirections[i] ? currentParameters[i] + LearningRate : currentParameters[i] - LearningRate);
                    parameterErrors[i] = currentError - ErrorFunction(dataBlock, currentParameters, Order, predictionFunction);
                    parameterErrors[i] = parameterErrors[i] < 0 ? 0 : parameterErrors[i];
                    currentParameters[i] = savedParameter;
                    if (maxErrorChange < parameterErrors[i])
                    {
                        maxErrorChange = parameterErrors[i];
                    }
                }
                if (maxErrorChange == 0)
                {
                    for (int i = 0; i < ParameterMovementDirections.Count; i++)
                        ParameterMovementDirections[i] = !ParameterMovementDirections[i];
                    if (wasLastZero)
                    {
                        LearningRate /= 2;
                        Console.WriteLine("Learning rate is now " + LearningRate);
                    }
                    wasLastZero = true;
                    currEpochs++;
                    continue;
                }
                for (int i = 0; i < currentParameters.Count; i++)
                {
                    currentParameters[i] = (float)(ParameterMovementDirections[i] ?
                        currentParameters[i] + (LearningRate * (parameterErrors[i] / maxErrorChange)) :
                        currentParameters[i] - (LearningRate * (parameterErrors[i] / maxErrorChange)));

                    if (parameterErrors[i] == 0)
                        ParameterMovementDirections[i] = !ParameterMovementDirections[i];
                }
                previousError = currentError;
                currentError = ErrorFunction(dataBlock, currentParameters, Order, predictionFunction);
                if (currentError > previousError)
                    LearningRate /= 2;
                else if (previousError - currentError < minimumErrorChange)
                    break;
                currEpochs++;
                if (wasLastZero)
                    wasLastZero = false;
            }
            for (int i = 0; i < initialParameters.Count; i++)
            {
                initialParameters[i] = currentParameters[i];
            }
        }
    }
}
