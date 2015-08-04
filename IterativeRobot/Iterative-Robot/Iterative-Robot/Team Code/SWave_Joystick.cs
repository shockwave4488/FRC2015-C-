using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Logitech attack 3 controller button enumeration
    /// </summary>
    public enum JoystickButtons
    {
        Trigger, Button2, Button3,
        Button4, Button5, Button6,
        Button7, Button8, Button9,
        Button10, Button11
    }

    /// <summary>
    /// Logitech attack 3 controller axis enumeration
    /// </summary>
    public enum JoystickAxes { X, Y, Z }

    /// <summary>
    /// Logitech attack 3 mapped joystick
    /// </summary>
    public class SWave_Joystick : Joystick
    {
        public SWave_Joystick(int port) : base(port) { }
        
        public bool getButton(JoystickButtons button)
        {
            return base.GetRawButton((int)button);
        }

        public double getAxis(JoystickAxes axis)
        {
            return base.GetRawAxis((int)axis);
        }
    }
}
