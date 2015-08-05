using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using Iterative_Robot.Team_Code;

namespace Iterative_Robot.SubSystems
{
    /// <summary>
    /// All primary button actions
    /// </summary>
    public enum PrimaryButtonControls
    {
        Reset_Gyro, Toggle_Field_Centric, Clear_Faults,
        Drive_Reduction, Output, Left_Align, Right_Align,
        Center, Tote_Ramp, Strafe_Left, Strafe_Right, Strafe_Down,
        Strafe_Up
    }

    /// <summary>
    /// All primary axis controls
    /// </summary>
    public enum PrimaryAxisControls { DriveX, DriveY, DriveRotate }

    /// <summary>
    /// All secondary button actions
    /// </summary>
    public enum SecondaryButtonControls
    {
        Auto_Stack, Arm_Up, Manual_Arm, Manual_Elevator,
        Claw_Open, Reset, Can_Burglar
    }

    /// <summary>
    /// Contains references for both primary and secondary joysticks
    /// </summary>
    public class Joysticks
    {
        private SWave_Joystick Primary_Left;
        private SWave_Joystick Primary_Right;
        private SWave_Xbox Secondary;
        
        public Joysticks()
        {
            Primary_Left = new SWave_Joystick(Constants.Primary_Left_Port);
            Primary_Right = new SWave_Joystick(Constants.Primary_Right_Port);
            Secondary = new SWave_Xbox(Constants.Secondary_Port);
        }

        public bool getPrimaryButton(PrimaryButtonControls action)
        {
            switch (action)
            {
                case PrimaryButtonControls.Reset_Gyro:
                    return Primary_Left.GetButton(Constants.Primary_Left_GyroReset);
                case PrimaryButtonControls.Toggle_Field_Centric:
                    return Primary_Left.GetButton(Constants.Primary_Left_ToggleFieldCentric);
                case PrimaryButtonControls.Clear_Faults:
                    return Primary_Left.GetButton(Constants.Primary_Left_ClearFaults);
                case PrimaryButtonControls.Drive_Reduction:
                    return Primary_Left.GetButton(Constants.Primary_Left_DriveReduction);
                case PrimaryButtonControls.Output:
                    return Primary_Left.GetButton(Constants.Primary_Left_Output);
                case PrimaryButtonControls.Left_Align:
                    return Primary_Left.GetButton(Constants.Primary_Left_AlignLeft);
                case PrimaryButtonControls.Right_Align:
                    return Primary_Left.GetButton(Constants.Primary_Left_AlignRight);
                case PrimaryButtonControls.Center:
                    return Primary_Left.GetButton(Constants.Primary_Left_AlignCenter);
                case PrimaryButtonControls.Tote_Ramp:
                    return Primary_Right.GetButton(Constants.Primary_Right_ToteRamp);
                case PrimaryButtonControls.Strafe_Left:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Left);
                case PrimaryButtonControls.Strafe_Right:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Right);
                case PrimaryButtonControls.Strafe_Down:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Down);
                case PrimaryButtonControls.Strafe_Up:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Up);
                default:
                    return false;
            }


        }

        public double getPrimaryAxis(PrimaryAxisControls action)
        {
            switch (action)
            {
                case PrimaryAxisControls.DriveX:
                    return Primary_Left.GetAxis(Constants.Primary_Drive_Strafe);
                case PrimaryAxisControls.DriveY:
                    return Primary_Left.GetAxis(Constants.Primary_Drive_ForwardBack);
                case PrimaryAxisControls.DriveRotate:
                    return Primary_Right.GetAxis(Constants.Primary_Drive_R);
                default:
                    return 0;
            }
        }
    }
}
