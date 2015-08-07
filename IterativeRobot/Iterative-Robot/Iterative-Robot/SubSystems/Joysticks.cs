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
        ResetGyro, ToggleFieldCentric, ClearFaults,
        DriveReduction, Output, LeftAlign, RightAlign,
        Center, ToteRamp, StrafeLeft, StrafeRight, StrafeDown,
        StrafeUp
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
        AutoStack, ArmUp, ManualArm, ManualElevator,
        ClawOpen, Reset, CanBurglar
    }

    public enum SecondaryAxisControls { ManualArm, ManualElevator }

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
            Primary_Left = new SWave_Joystick(Constants.ChannelUSB_PrimaryLeft);
            Primary_Right = new SWave_Joystick(Constants.ChannelUSB_PrimaryRight);
            Secondary = new SWave_Xbox(Constants.ChannelUSB_Secondary);
        }

        public bool GetPrimaryButton(PrimaryButtonControls action)
        {
            switch (action)
            {
                case PrimaryButtonControls.ResetGyro:
                    return Primary_Left.GetButton(Constants.Primary_Left_GyroReset);
                case PrimaryButtonControls.ToggleFieldCentric:
                    return Primary_Left.GetButton(Constants.Primary_Left_ToggleFieldCentric);
                case PrimaryButtonControls.ClearFaults:
                    return Primary_Left.GetButton(Constants.Primary_Left_ClearFaults);
                case PrimaryButtonControls.DriveReduction:
                    return Primary_Left.GetButton(Constants.Primary_Left_DriveReduction);
                case PrimaryButtonControls.Output:
                    return Primary_Left.GetButton(Constants.Primary_Left_Output);
                case PrimaryButtonControls.LeftAlign:
                    return Primary_Left.GetButton(Constants.Primary_Left_AlignLeft);
                case PrimaryButtonControls.RightAlign:
                    return Primary_Left.GetButton(Constants.Primary_Left_AlignRight);
                case PrimaryButtonControls.Center:
                    return Primary_Left.GetButton(Constants.Primary_Left_AlignCenter);
                case PrimaryButtonControls.ToteRamp:
                    return Primary_Right.GetButton(Constants.Primary_Right_ToteRamp);
                case PrimaryButtonControls.StrafeLeft:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Left);
                case PrimaryButtonControls.StrafeRight:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Right);
                case PrimaryButtonControls.StrafeDown:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Down);
                case PrimaryButtonControls.StrafeUp:
                    return Primary_Right.GetButton(Constants.Primary_Right_Strafe_Up);
                default:
                    return false;
            }


        }

        public double GetPrimaryAxis(PrimaryAxisControls action)
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

        public bool GetSecondaryButton(SecondaryButtonControls action)
        {
            switch (action)
            {
                case SecondaryButtonControls.AutoStack:
                    return Secondary.getButton(Constants.Secondary_AutoStack);
                case SecondaryButtonControls.ArmUp:
                    return Secondary.getButton(Constants.Secondary_ArmUp);
                case SecondaryButtonControls.ManualArm:
                    return Secondary.getButton(Constants.Secondary_ManualArmEnable);
                case SecondaryButtonControls.ManualElevator:
                    return Secondary.getButton(Constants.Secondary_ManualElevatorEnable);
                case SecondaryButtonControls.ClawOpen:
                    return Secondary.getButton(Constants.Secondary_ClawOpen);
                case SecondaryButtonControls.Reset:
                    return Secondary.getButton(Constants.Secondary_Reset);
                case SecondaryButtonControls.CanBurglar:
                    return Secondary.getButton(Constants.Secondary_CanBurglar);
                default:
                    return false;
            }
        }

        public double GetSecondaryAxis(SecondaryAxisControls action)
        {
            switch (action)
            {
                case SecondaryAxisControls.ManualArm:
                    return Secondary.getAxis(Constants.Secondary_ManualArm);
                case SecondaryAxisControls.ManualElevator:
                    return Secondary.getAxis(Constants.Secondary_ManualElevator);
                default:
                    return 0;
            }
        }
    }
}
