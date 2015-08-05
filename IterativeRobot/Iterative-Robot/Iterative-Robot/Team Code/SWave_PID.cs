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
    class SWave_PID : SWave_IStandard<double>
    {
        private double P, I, D;
        private double accumulatedIntegral;
        private double currentPointFeedback;

        public bool Enabled { get; set; }
        public string Name { get; set; }
        
        public double setpoint { get; set; }

        public SWave_PID(double P_, double I_, double D_)
        {
            P = P_; I = I_; D = D_; Name = "Anonymous"; Enabled = true; accumulatedIntegral = 0; currentPointFeedback = 0;
        }

        public double get(double currentPoint)
        {
            return ((setpoint - currentPoint) * P) + ((currentPoint - currentPointFeedback) * -D) + accumulatedIntegral;
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
    }
}
