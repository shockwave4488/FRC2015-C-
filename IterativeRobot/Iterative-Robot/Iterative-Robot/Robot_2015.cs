using System;
using System.Collections.Generic;
using System.Linq;
using WPILib;
using Iterative_Robot.SubSystems;

namespace Iterative_Robot
{
    /**
     * The VM is configured to automatically run this class, and to call the
     * functions corresponding to each mode, as described in the IterativeRobot
     * documentation. 
     */
    public class Robot2015 : IterativeRobot
    {
        Joysticks joysticks;
        Drive mecDrive; 
        //Compressor compressor;
        Elevator elevator;
        ToteChute toteChute;
        Arm arm;

        /**
         * This function is run when the robot is first started up and should be
         * used for any initialization code.
         */
        public override void RobotInit()
        {
            Constants.initArmLocations();
            Constants.initLiftLocations();

            joysticks = new Joysticks();
            mecDrive = new Drive();
            elevator = new Elevator();
            toteChute = new ToteChute();
            arm = new Arm();

            //compressor = new Compressor();

            //compressor.Start();
        }

        /**
         * This function is called periodically during autonomous
         */
        public override void AutonomousPeriodic()
        {
            
        }

        /**
         * This function is called periodically during operator control
         */
        public override void TeleopPeriodic()
        {
            mecDrive.X = joysticks.GetPrimaryAxis(PrimaryAxisControls.DriveX);
            mecDrive.Y = joysticks.GetPrimaryAxis(PrimaryAxisControls.DriveY);
            mecDrive.Rotation = joysticks.GetPrimaryAxis(PrimaryAxisControls.DriveRotate);

            mecDrive.FieldCentric = joysticks.GetPrimaryButton(PrimaryButtonControls.ToggleFieldCentric);

            mecDrive.Update(null); //Debug this by Disabling X Movement.
        }

        /**
         * This function is called periodically during test mode
         */
        public override void TestPeriodic()
        {
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

            if (joysticks.GetSecondaryButton(SecondaryButtonControls.ArmUp))
                arm.loc = ArmLocation.High;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.AutoStack))
                arm.loc = ArmLocation.Low;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.Reset))

                arm.loc = ArmLocation.Release;
            else if (joysticks.GetSecondaryButton(SecondaryButtonControls.ManualArm))
                arm.Enabled = false;

            arm.clawState = joysticks.GetSecondaryButton(SecondaryButtonControls.ClawOpen);
            /*
            //tote Chute Testing
            toteChute.RampIn = joysticks.GetPrimaryButton(PrimaryButtonControls.ToteRamp);
            toteChute.Stopper = joysticks.GetPrimaryButton(PrimaryButtonControls.DriveReduction);
            toteChute.output = joysticks.GetPrimaryButton(PrimaryButtonControls.Output);
            */

            //elevator.Update(joysticks.GetSecondaryAxis(SecondaryAxisControls.ManualElevator));
            //toteChute.Update(null);
            arm.Update(joysticks.GetSecondaryAxis(SecondaryAxisControls.ManualArm));
            //WPILib.SmartDashboards.SmartDashboard.PutString("Tote Chute Status", toteChute.Print());
        }

    }
}
