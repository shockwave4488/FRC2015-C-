using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Iterative_Robot.Team_Code;

namespace Iterative_Robot
{
    /// <summary>
    /// const class defines all global constants for this program.
    /// PWM/DIO/SOlenoid Channels, Setpoints, Button Mappings, Etc.
    /// </summary>
    static class Constants
    {
        static Constants()
        {
            initLiftLocations();
            initArmLocations();
        }

        //PWM Channels
        public const int ChannelPWM_DriveLF = 0;
        public const int ChannelPWM_DriveLR = 1;
        public const int ChannelPWM_DriveRF = 3;
        public const int ChannelPWM_DriveRR = 2;
        public const int ChannelPWM_Lift = 7;
        public const int ChannelPWM_Arm = 5;
        public const int ChannelPWM_Output = 4;

        //Solenoid Channels
        public const int ChannelSolenoid_ClawF = 0;
        public const int ChannelSolenoid_ClawR = 1;
        public const int ChannelSolenoid_RampF = 2;
        public const int ChannelSolenoid_RampR = 3;
        public const int ChannelSolenoid_CanBurglarF = 4;
        public const int ChannelSolenoid_CanBurglarR = 5;
        public const int ChannelSolenoid_StopF = 6;
        public const int ChannelSolenoid_StopR = 7;

        //Digital IO Channels
        public const int ChannelDIO_BeamBack = 23;
        public const int ChannelDIO_BeamFront = 22;
        public const int ChannelDIO_HallEffect = 9;

        //Analogue Sensor Channels
        public const int ChannelAnalogue_Gyro = 0;
        public const int ChannelAnalogue_BackUltrasonic = 1;
        public const int ChannelAnalogue_SideUltrasonic = 2;
        public const int ChannelAnalogue_LiftPot = 4;
        public const int ChannelAnalogue_ArmPot = 5;

        //Joystick Channels
        public const int ChannelUSB_PrimaryLeft = 0;
        public const int ChannelUSB_PrimaryRight = 1;
        public const int ChannelUSB_Secondary = 2;

        //Primary Driver Left Button Controls
        public const JoystickButtons Primary_Left_DriveReduction = JoystickButtons.Trigger;
        public const JoystickButtons Primary_Left_Output = JoystickButtons.Button2;
        public const JoystickButtons Primary_Left_AlignCenter = JoystickButtons.Button3;
        public const JoystickButtons Primary_Left_AlignLeft = JoystickButtons.Button4;
        public const JoystickButtons Primary_Left_AlignRight = JoystickButtons.Button5;
        public const JoystickButtons Primary_Left_ClearFaults = JoystickButtons.Button6;
        public const JoystickButtons Primary_Left_GyroReset = JoystickButtons.Button7;
        public const JoystickButtons Primary_Left_ToggleFieldCentric = JoystickButtons.Button11;

        //Primary Driver Right Button Controls
        public const JoystickButtons Primary_Right_ToteRamp = JoystickButtons.Trigger;
        public const JoystickButtons Primary_Right_Strafe_Down = JoystickButtons.Button2;
        public const JoystickButtons Primary_Right_Strafe_Up = JoystickButtons.Button3;
        public const JoystickButtons Primary_Right_Strafe_Right = JoystickButtons.Button5;
        public const JoystickButtons Primary_Right_Strafe_Left = JoystickButtons.Button4;

        //Primary Driver Axis Controls
        public const JoystickAxes Primary_Drive_Strafe = JoystickAxes.X;
        public const JoystickAxes Primary_Drive_ForwardBack = JoystickAxes.Y;
        public const JoystickAxes Primary_Drive_R = JoystickAxes.X;

        //Secondary Driver Button Controls
        public const XboxButtons Secondary_AutoStack = XboxButtons.A;
        public const XboxButtons Secondary_ArmUp = XboxButtons.Right_Bumper;
        public const XboxButtons Secondary_ManualArmEnable = XboxButtons.Start;
        public const XboxButtons Secondary_ManualElevatorEnable = XboxButtons.Back;
        public const XboxButtons Secondary_ClawOpen = XboxButtons.X;
        public const XboxButtons Secondary_Reset = XboxButtons.Y;
        public const XboxButtons Secondary_Pause = XboxButtons.B;
        public const XboxButtons Secondary_CanBurglar = XboxButtons.Left_Joystick;

        //Secondary Driver Axis Controls
        public const XboxAxes Secondary_ManualArm = XboxAxes.Left_X;
        public const XboxAxes Secondary_ManualElevator = XboxAxes.Right_Y;

        //Misc Variables
        public const double UltraScaling = 0.0049;

        //Drive Variables
        public const double Drive_AccelLimit = 0.2;
        public const double Drive_TurnP = 0.02;
        public const double Drive_TurnD = 0;
        public const double Drive_AlignBackP = 0.02;
        public const double Drive_AlignBackD = 0;
        public const double Drive_AlignBackSetLoad = 83;
        public const double Drive_AlignBackSetNoodle = 40;
        public const double Drive_AlignBackSetOutput = 80;
        public const double Drive_AlignBackSetGetCan = 40;
        public const double Drive_AlignSideP = 0.02;
        public const double Drive_AlignSideD = 0;
        public const double Drive_AlignSideSetpoint = 46;
        public const double Drive_StrafeButtonSpeed = 0.75;
        public const double Drive_ForwardButtonSpeed = 0.5;
        public const double Drive_AlignLoadAngle = 53;

        //Conveyor Variables
        public const double Conveyor_OutputSpeed = 0.5;
        public const double Conveyor_InSpeed = 0.1;
        public const double Conveyor_OutSpeed = 0.1;

        //Lift Setpoints
        private const double LiftPickupLoc = 0.062;
        private const double LiftBottomLoc = 0.026;
        private const double LiftStabilizeLoc = 0.125;
        private const double LiftHighLoc = 0.41;
        private const double LiftFirstLoc = 0.25;

        public static Dictionary<SubSystems.ElevatorLocation, double> Lift_Locations = new Dictionary<SubSystems.ElevatorLocation, double>();

        public static void initLiftLocations()
        {
            Lift_Locations.Add(SubSystems.ElevatorLocation.Pickup, LiftPickupLoc);
            Lift_Locations.Add(SubSystems.ElevatorLocation.Bottom, LiftBottomLoc);
            Lift_Locations.Add(SubSystems.ElevatorLocation.Stabilize, LiftStabilizeLoc);
            Lift_Locations.Add(SubSystems.ElevatorLocation.High, LiftHighLoc);
            Lift_Locations.Add(SubSystems.ElevatorLocation.First_Tote, LiftFirstLoc);
        }

        //Lift Variables
        public const double Lift_LimitHigh = 0.5;
        public const double Lift_LimitLow = 0.025;
        public const double Lift_P = 8;
        public const double Lift_D = 0;
        public const double Lift_PosnTolerance = 0.04;

        //Arm Setpoints
        private const double ArmLowLoc = 0.623;
        private const double ArmHighLoc = 0.257;
        private const double ArmReleaseLoc = 0.18;

        public static Dictionary<SubSystems.ArmLocation, double> ArmLocations = new Dictionary<SubSystems.ArmLocation, double>();

        public static void initArmLocations()
        {
            ArmLocations.Add(SubSystems.ArmLocation.High, ArmHighLoc);
            ArmLocations.Add(SubSystems.ArmLocation.Low, ArmLowLoc);
            ArmLocations.Add(SubSystems.ArmLocation.Release, ArmReleaseLoc);
        }

        //Arm Variables
        public const double Arm_LimitLow = 0.18;
        public const double Arm_LimitHigh = 0.65;
        public const double Arm_P = 5.5;
        public const double Arm_D = 0;
        public const double Arm_PosnTolerance = 0.05;
    }
}
