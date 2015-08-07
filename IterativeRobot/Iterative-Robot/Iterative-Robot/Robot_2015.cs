using System;
using System.Collections.Generic;
using System.Linq;
using WPILib;
using Iterative_Robot.SubSystems;
using Iterative_Robot.Systems;
using Iterative_Robot.Team_Code;

namespace Iterative_Robot
{
    /**
     * The VM is configured to automatically run this class, and to call the
     * functions corresponding to each mode, as described in the IterativeRobot
     * documentation. 
     */
    public class Robot2015 : IterativeRobot
    {
        private Joysticks joysticks;
        //Drive mecDrive; 
        private Compressor compressor;
        private SmartDrive drive;
        private Stacker stacker;
        private CanBurglar canburglar;

        private SWave_WaitByCallCount AutonDriveWait;
        private bool grabbed_once;

        //Elevator elevator;
        //ToteChute toteChute;
        //Arm arm;

        /**
         * This function is run when the robot is first started up and should be
         * used for any initialization code.
         */
        public override void RobotInit()
        {
            Constants.initArmLocations();
            Constants.initLiftLocations();
            AutonDriveWait = new SWave_WaitByCallCount(75);

            joysticks = new Joysticks();
            drive = new SmartDrive();

            stacker = new Stacker();

            canburglar = new CanBurglar();

            compressor = new Compressor();

            compressor.Start();
        }


        public override void DisabledPeriodic()
        {
            WPILib.SmartDashboards.SmartDashboard.PutString("Gyro Value", drive.writeGyro());
        }

        /**
         * This function is called periodically during autonomous
         */
        public override void AutonomousPeriodic()
        {
            canburglar.grab = true;
            AutonDriveWait.Update(true);

            grabbed_once = canburglar.grabbed || grabbed_once;

            if(AutonDriveWait.WaitComplete || grabbed_once)
                drive.DriveSpeeds = new point(0, 1);
            else
                drive.DriveSpeeds = new point(0, 0);
        }

        public override void TeleopInit()
        {
            stacker.Start();
        }

        /**
         * This function is called periodically during operator control
         */
        public override void TeleopPeriodic()
        {
            //Drive
            drive.DriveSpeeds = new point(joysticks.GetPrimaryAxis(PrimaryAxisControls.DriveX), joysticks.GetPrimaryAxis(PrimaryAxisControls.DriveY));
            drive.Rotation = joysticks.GetPrimaryAxis(PrimaryAxisControls.DriveRotate);

            drive.StrafeBackButton = joysticks.GetPrimaryButton(PrimaryButtonControls.StrafeDown);
            drive.StrafeForwardButton = joysticks.GetPrimaryButton(PrimaryButtonControls.StrafeUp);
            drive.StrafeLeftButton = joysticks.GetPrimaryButton(PrimaryButtonControls.StrafeLeft);
            drive.StrafeRightButton = joysticks.GetPrimaryButton(PrimaryButtonControls.StrafeRight);

            drive.FieldCentric = joysticks.GetPrimaryButton(PrimaryButtonControls.ToggleFieldCentric);

            //Stacker
            if (joysticks.GetPrimaryButton(PrimaryButtonControls.ResetGyro))
                drive.resetGyro();

            if (joysticks.GetSecondaryButton(SecondaryButtonControls.AutoStack))
                stacker.state = StackerState.AlignTote;
            if (joysticks.GetSecondaryButton(SecondaryButtonControls.Reset))
                stacker.Reset();

            stacker.claw = joysticks.GetSecondaryButton(SecondaryButtonControls.ClawOpen);
            stacker.ArmDown = joysticks.GetSecondaryButton(SecondaryButtonControls.ArmUp);
            stacker.output = joysticks.GetPrimaryButton(PrimaryButtonControls.Output);

            //stacker.Update(null);
            drive.update();

            WPILib.SmartDashboards.SmartDashboard.PutString("Rotate Edge Trigger", drive.writeEdge());
        }

        /**
         * This function is called periodically during test mode
         */
        public override void TestPeriodic()
        {
            if (joysticks.GetSecondaryButton(SecondaryButtonControls.AutoStack))
                stacker.state = StackerState.AlignTote;
            if (joysticks.GetSecondaryButton(SecondaryButtonControls.Reset))
                stacker.Reset();

            stacker.claw = joysticks.GetSecondaryButton(SecondaryButtonControls.ClawOpen);
            stacker.ArmDown = joysticks.GetSecondaryButton(SecondaryButtonControls.ArmUp);
            stacker.output = joysticks.GetPrimaryButton(PrimaryButtonControls.Output);

            stacker.Update(null);
            WPILib.SmartDashboards.SmartDashboard.PutString("Stacker", stacker.Print());

            /*
            //Elevator Testing
            if (joysticks.GetSecondaryButton(SecondaryButtonControls.Auto_Stack))
                elevator.loc = ElevatorLocation.Bottom;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.Claw_Open))
                elevator.loc = ElevatorLocation.Pickup;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.Reset))
                elevator.loc = ElevatorLocation.Stabilize;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.Arm_Up))
                elevator.loc = ElevatorLocation.First_Tote;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.Manual_Arm))
                elevator.loc = ElevatorLocation.High;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.Manual_Elevator))
                elevator.Enabled = false;
            */

            /*
            if (joysticks.GetSecondaryButton(SecondaryButtonControls.ArmUp))
                arm.loc = ArmLocation.High;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.AutoStack))
                arm.loc = ArmLocation.Low;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.Reset))
                arm.loc = ArmLocation.Release;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.ManualArm))
                arm.Enabled = false;
                */

            //arm.clawState = joysticks.GetSecondaryButton(SecondaryButtonControls.ClawOpen);
            
            /*
            //tote Chute Testing
            toteChute.RampIn = joysticks.GetPrimaryButton(PrimaryButtonControls.ToteRamp);
            toteChute.Stopper = joysticks.GetPrimaryButton(PrimaryButtonControls.DriveReduction);
            toteChute.output = joysticks.GetPrimaryButton(PrimaryButtonControls.Output);
            */

            //elevator.Update(joysticks.GetSecondaryAxis(SecondaryAxisControls.ManualElevator));
            //toteChute.Update(null);
            //arm.Update(joysticks.GetSecondaryAxis(SecondaryAxisControls.ManualArm));

            //WPILib.SmartDashboards.SmartDashboard.PutString("Arm status", arm.Print());
            //WPILib.SmartDashboards.SmartDashboard.PutString("Tote Chute Status", toteChute.Print());
        }

    }
}
