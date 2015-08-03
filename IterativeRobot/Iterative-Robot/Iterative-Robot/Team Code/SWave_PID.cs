using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    class SWave_PID
    {
        double P, I, D;
        double currentPointFeedback;
        
        public double setpoint { get; set; }

        public SWave_PID(double P_, double I_, double D_)
        {
            P = P_; I = I_; D = D_;
        }

        public double get(double currentPoint)
        {
            return ((setpoint - currentPoint) * P) + ((currentPoint - currentPointFeedback) * -D);
        }

        public void update(double currentPoint)
        {
            currentPointFeedback = currentPoint;
        }
    }
}
