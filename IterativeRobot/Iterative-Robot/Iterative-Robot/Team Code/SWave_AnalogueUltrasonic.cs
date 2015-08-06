using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace Iterative_Robot.Team_Code
{
    class SWave_AnalogueUltrasonic : AnalogInput
    {
        private double scalingFactor;

        public SWave_AnalogueUltrasonic(int channel, double scaleFactor) : base(channel)
        {
            scalingFactor = scaleFactor;
        }

        public double get()
        {
            return GetAverageVoltage() / scalingFactor;
        }
    }
}
