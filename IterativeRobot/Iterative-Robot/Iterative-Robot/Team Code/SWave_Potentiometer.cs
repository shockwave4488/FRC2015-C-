using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Potentioneter wrapped in a class implementing SWave_IPositionSensor for use in the SWave_PIDSubsystem Class
    /// </summary>
    class SWave_Potentiometer : AnalogPotentiometer, SWave_IPositionSensor
    {
        public SWave_Potentiometer(AnalogInput input) : base(input) { }
        public SWave_Potentiometer(int channel) : base(channel) { }
        public SWave_Potentiometer(AnalogInput input, double scale) : base(input, scale) { }
        public SWave_Potentiometer(int channel, double scale) : base(channel, scale) { }
        public SWave_Potentiometer(AnalogInput input, double fullRange, double offset) : base(input, fullRange, offset) { }
        public SWave_Potentiometer(int channel, double fullRange, double offset) : base(channel, fullRange, offset) { }

        public double get()
        {
            return base.Get();
        }
    }
}
