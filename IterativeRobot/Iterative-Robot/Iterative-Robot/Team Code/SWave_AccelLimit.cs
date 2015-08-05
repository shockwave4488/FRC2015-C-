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
    class SWave_AccelLimit : SWave_IStandard<double>
    {
        private double feedback;

        public double dOut { get; set; }
        public bool LimitAccel { get; set; }
        public bool LimitDeccel { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public SWave_AccelLimit(double dOut_) : this(dOut_, true, true) { }
        public SWave_AccelLimit(double dOut_, bool LimitAccel_, bool LimitDeccel_)
        {
            feedback = 0; dOut = dOut_; LimitAccel = LimitAccel_; LimitDeccel = LimitDeccel_; Name = "Anonymous"; Enabled = true;
        }

        public double Get()
        {
            return feedback;
        }

        public void Update(double setpoint)
        {
            if (double.IsNaN(feedback)) //Because if this were not here and feedback became nan, reboot would be required.
                feedback = 0;
            else if (!Enabled)
                feedback = setpoint;
            else if (LimitAccel && Math.Abs(setpoint) > Math.Abs(feedback))
                feedback = Math.Min(setpoint, feedback + dOut);
            else if (LimitDeccel && Math.Abs(setpoint) < Math.Abs(feedback))
                feedback = Math.Max(setpoint, feedback - dOut);
            else
                feedback = setpoint;
        }

        public string Print()
        {
            string toReturn = Name + " " + GetType();

            if (LimitAccel || LimitDeccel)
            {
                toReturn += "\n\tLimiting ";
                if (LimitAccel)
                    toReturn += "Acceleration";
                if (LimitAccel && LimitDeccel)
                    toReturn += " and ";
                if (LimitDeccel)
                    toReturn += "Decceleration";
            }
            else toReturn += "\n\tNot Limiting Acceleration or Decceleration";

            toReturn += "\n\t" + (Enabled ? "" : "Not ") + "Enabled";
            toReturn += "\n\tChange in delta per tick : " + dOut;
            toReturn += "\n\tCurrent Feedback : " + feedback;

            return toReturn;
        }
    }
}
