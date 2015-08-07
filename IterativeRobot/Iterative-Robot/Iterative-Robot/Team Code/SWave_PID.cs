using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Simpler form of PID logic
    /// </summary>
    public class SWave_PID : SWave_IStandard<double>
    {
        private double P, I, D;
        private double accumulatedIntegral;
        private double currentPointFeedback;

        public bool Enabled { get; set; }
        public string Name { get; set; }

        public double Max { get; set; }
        public double Min { get; set; }
        public double setpoint { get; set; }

        public SWave_PID(double p, double i, double d, double min, double max)
        {
            if(max < min)
                throw new Exception("Invalid Arguments: " + max + " Is less than " + min);

            P = p; I = i; D = d;
            Name = "Anonymous";
            Enabled = true;
            accumulatedIntegral = 0;
            currentPointFeedback = 0;
            Max = max;
            Min = min;
        }

        public SWave_PID(double p, double i, double d) : this(p, i, d, double.MinValue, double.MaxValue) { }

        public double get(double currentPoint)
        {
            return limit(((setpoint - currentPoint) * P) + ((currentPoint - currentPointFeedback) * -D) + accumulatedIntegral);
        }

        public void Update(double currentPoint)
        {
            if (I != 0) accumulatedIntegral += (currentPoint - currentPointFeedback) * I;
            if (D != 0) currentPointFeedback = currentPoint;
        }

        public string Print()
        {
            string toReturn = Name + " " + GetType();

            toReturn += ("\n\tkP : " + P + "\n\tkI : " + I + "\n\tkD : " + D);

            if (I != 0)
                toReturn += "\n\tAccumulated Integral : " + accumulatedIntegral;
            if (D != 0)
                toReturn += "\n\tDerivative Feedback : " + currentPointFeedback;

            return toReturn;
        }

        private double limit(double limitIn)
        {
            limitIn = limitIn > Max ? Max : limitIn;
            limitIn = limitIn < Min ? Min : limitIn;
            return limitIn;
        }
    }
}
