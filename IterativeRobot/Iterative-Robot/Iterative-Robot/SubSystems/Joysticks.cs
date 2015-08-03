using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.SubSystems
{
    public enum PrimaryControls
    {
        Reset_Gyro, Toggle_Field_Centric, Clear_Faults,
        Drive_Reduction, Output, Left_Align, Right_Align,
        Center, Tote_Ramp, Strafe_Left, Strafe_Right, Strafe_Down,
        Strafe_Up
    }
    public enum SecondaryControls
    {
        Auto_Stack, Arm_Up, Manual_Arm, Manual_Elevator,
        Claw_Open, Reset, Can_Burglar
    }

    public class Joysticks : Subsystem
    {
        private Joystick Primary_Left;
        private Joystick Primary_Right;
        private Joystick Secondary;
        
        public Joysticks()
        {
            Primary_Left = new Joystick(Constants.Primary_Left_Port);
            Primary_Right = new Joystick(Constants.Primary_Right_Port);
            Secondary = new Joystick(Constants.Secondary_Port);
        }

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }
    }
}
