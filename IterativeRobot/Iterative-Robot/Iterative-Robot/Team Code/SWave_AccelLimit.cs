using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Limits the amount of acceleration per update by power.
    /// Useful for high power drive trains and high torque mechanisms
    /// </summary>
    class SWave_AccelLimit
    {
        double dOut { get; set; }
        public double feedback;
        
        public SWave_AccelLimit(double dOut)
        {
            feedback = 0;
        }

        public double Get(double setpoint)
        {
            feedback = Math.Min(Math.Max(setpoint, feedback - dOut), feedback + dOut);
            return feedback;
        }
    }
}
