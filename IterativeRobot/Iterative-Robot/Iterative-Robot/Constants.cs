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
    /// Static class defines all global constants for this program.
    /// PWM/DIO/SOlenoid Channels, Setpoints, Button Mappings, Etc.
    /// </summary>
    static class Constants
    {
        //PWM Channels
        public static int DriveLFPort = 2;
        public static int DriveLRPort = 0;
        public static int DriveRFPort = 3;
        public static int DriveRRPort = 1;
        public static int LiftPort = 5;
        public static int ArmPort = 7;
        public static int ConveyorPort = 4;

        //Solenoid Channels
        public static int ClawChannel_Forward = 0;
        public static int ClawChannel_Reverse = 1;
        public static int RampChannel_Forward = 2;
        public static int RampChannel_Reverse = 3;
        public static int Can_Burglar_Forward = 4;
        public static int Can_Burglar_Reverse = 5;
        public static int StopChannel_Forward = 6;
        public static int StopChannel_Reverse = 7;

        //Digital IO Channels
        public static int BeamBackChannel = 13;
        public static int BeamFrontChannel = 12;
        public static int HallEffectChannel = 9;

        //Analogue Sensor Channels
        public static int LiftPotChannel = 4;
        public static int ArmPotChannel = 5;

        //Joystick Channels
        public static int Primary_Left_Port = 0;
        public static int Primary_Right_Port = 1;
        public static int Secondary_Port = 2;

        //Primary Driver Left Button Controls
        public static JoystickButtons Primary_Left_DriveReduction = JoystickButtons.Trigger;
        public static JoystickButtons Primary_Left_Output = JoystickButtons.Button2;
        public static JoystickButtons Primary_Left_AlignCenter = JoystickButtons.Button3;
        public static JoystickButtons Primary_Left_AlignLeft = JoystickButtons.Button4;
        public static JoystickButtons Primary_Left_AlignRight = JoystickButtons.Button5;
        public static JoystickButtons Primary_Left_ClearFaults = JoystickButtons.Button6;
        public static JoystickButtons Primary_Left_GyroReset = JoystickButtons.Button7;
        public static JoystickButtons Primary_Left_ToggleFieldCentric = JoystickButtons.Button11;

        //Primary Driver Axis Controls
        public static JoystickAxes Primary_Drive_Strafe = JoystickAxes.Y;
        public static JoystickAxes Primary_Drive_ForwardBack = JoystickAxes.X;
        public static JoystickAxes Primary_Drive_R = JoystickAxes.X;

        //Primary Driver Right Button Controls
        public static JoystickButtons Primary_Right_ToteRamp = JoystickButtons.Trigger;
        public static JoystickButtons Primary_Right_Strafe_Down = JoystickButtons.Button2;
        public static JoystickButtons Primary_Right_Strafe_Up = JoystickButtons.Button3;
        public static JoystickButtons Primary_Right_Strafe_Right = JoystickButtons.Button5;
        public static JoystickButtons Primary_Right_Strafe_Left = JoystickButtons.Button4;

        //Secondary Driver Button Controls
        public static XboxButtons Secondary_AutoStack = XboxButtons.A;
        public static XboxButtons Secondary_ArmUp = XboxButtons.Left_Bumper;
        public static XboxButtons Secondary_ManualClaw = XboxButtons.Start;
        public static XboxButtons Secondary_ManualElevator = XboxButtons.Back;
        public static XboxButtons Secondary_ClawOpen = XboxButtons.X;
        public static XboxButtons Secondary_Reset = XboxButtons.Y;
        public static XboxButtons Secondary_Pause = XboxButtons.B;
        public static XboxButtons Secondary_CanBurglar = XboxButtons.Left_Joystick;

        //Misc Variables
        public static double DriveAccelLimit = 0.2;

        //Lift Setpoints
        private static double LiftPickupLoc = 0.062;
        private static double LiftBottomLoc = 0.026;
        private static double LiftStabilizeLoc = 0.125;
        private static double LiftHighLoc = 0.41;
        private static double LiftFirstLoc = 0.25;

        public static Dictionary<SubSystems.ElevatorLocation, double> LiftLocations = new Dictionary<SubSystems.ElevatorLocation, double>();

        public static void initLiftLocations()
        {
            LiftLocations.Add(SubSystems.ElevatorLocation.Pickup, LiftPickupLoc);
            LiftLocations.Add(SubSystems.ElevatorLocation.Bottom, LiftBottomLoc);
            LiftLocations.Add(SubSystems.ElevatorLocation.Stabilize, LiftStabilizeLoc);
            LiftLocations.Add(SubSystems.ElevatorLocation.High, LiftHighLoc);
            LiftLocations.Add(SubSystems.ElevatorLocation.First_Tote, LiftFirstLoc);
        }

        //Lift Variables
        public static double LiftLimitHigh = 0.5;
        public static double LiftLimitLow = 0.025;
        public static double LiftP = 8;
        public static double LiftD = 0;
        public static double LiftPosnTolerance = 0.04;

        //Arm Setpoints
        private static double ArmLowLoc = 0.643;
        private static double ArmHighLoc = 0.272;
        private static double ArmReleaseLoc = 0.18;

        public static Dictionary<SubSystems.ArmLocation, double> ArmLocations = new Dictionary<SubSystems.ArmLocation, double>();

        public static void initArmLocations()
        {
            ArmLocations.Add(SubSystems.ArmLocation.High, ArmHighLoc);
            ArmLocations.Add(SubSystems.ArmLocation.Low, ArmLowLoc);
            ArmLocations.Add(SubSystems.ArmLocation.Release, ArmReleaseLoc);
        }

        //Arm Variables
        public static double ArmLimitLow = 0.18;
        public static double ArmLimitHigh = 0.65;
        public static double ArmP = 5.5;
        public static double ArmD = 0;
        public static double ArmPosnTolerance = 0.05;
    }
}
