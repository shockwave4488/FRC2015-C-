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
        Compressor compressor;

        /**
         * This function is run when the robot is first started up and should be
         * used for any initialization code.
         */
        public override void RobotInit()
        {
            joysticks = new Joysticks();
            mecDrive = new Drive();
            compressor = new Compressor();

            //compressor.Start();

            Constants.initLiftLocations();
            Constants.initArmLocations();
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
            mecDrive.X = joysticks.getPrimaryAxis(PrimaryAxisControls.DriveX);
            mecDrive.Y = joysticks.getPrimaryAxis(PrimaryAxisControls.DriveY);
            mecDrive.Rotation = joysticks.getPrimaryAxis(PrimaryAxisControls.DriveRotate);

            mecDrive.FieldCentric = joysticks.getPrimaryButton(PrimaryButtonControls.Toggle_Field_Centric);

            mecDrive.Update(null); //Debug this by Disabling X Movement.
        }

        /**
         * This function is called periodically during test mode
         */
        public override void TestPeriodic()
        {

        }

    }
}
