using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Xbox Mapped Button Enumerator
    /// </summary>
    public enum XboxButtons { A, B, X, Y, Left_Bumper, Right_Bumper, Back, Start, Left_Joystick, Right_Joystick }

    /// <summary>
    /// Xbox Mapped Controller
    /// </summary>
    public class SWave_Xbox : Joystick
    {
        public SWave_Xbox(int port) : base(port) { }

        public bool get(XboxButtons button)
        {
            return base.GetRawButton((int)button);
        }
    }
}
