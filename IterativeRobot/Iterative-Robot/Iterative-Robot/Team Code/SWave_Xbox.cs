using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Xbox mapped button enumerator
    /// </summary>
    public enum XboxButtons { A, B, X, Y, Left_Bumper, Right_Bumper, Back, Start, Left_Joystick, Right_Joystick }
    
    /// <summary>
    /// Xbos mapped axis enumerator
    /// </summary>
    public enum XboxAxes { Left_X, Left_Y, Left_Trigger, Right_Trigger, Right_X, Right_Y}

    /// <summary>
    /// Xbox Mapped Controller
    /// </summary>
    public class SWave_Xbox : Joystick
    {
        public SWave_Xbox(int port) : base(port) { }

        public bool getButton(XboxButtons button)
        {
            return base.GetRawButton((int)button);
        }

        public double getAxis(XboxAxes axis)
        {
            return base.GetRawAxis((int)axis);
        }
    }
}
