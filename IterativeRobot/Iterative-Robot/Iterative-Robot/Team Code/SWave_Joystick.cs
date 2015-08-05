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
        public double DeadZone { get; set; }
        public SWave_Joystick(int port) : base(port)
        {
            DeadZone = 0.1;
        }
        
        public bool GetButton(JoystickButtons button)
        {
            return base.GetRawButton((int)button);
        }

        public double GetAxis(JoystickAxes axis)
        {
            switch (axis)
            {
                case JoystickAxes.Y:
                    return fixDeadZone(-base.GetRawAxis((int)axis));
                case JoystickAxes.X:
                    return fixDeadZone(base.GetRawAxis((int)axis));
                default:
                    return base.GetRawAxis((int)axis);
            }
        }

        private double fixDeadZone(double valueIn)
        {
            return (Math.Abs(valueIn) < DeadZone ? 0 : valueIn);
        }
    }
}
