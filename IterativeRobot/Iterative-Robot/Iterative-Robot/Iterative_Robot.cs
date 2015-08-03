using System;
using System.Collections.Generic;
using System.Linq;
using WPILib;

namespace Iterative_Robot
{
    /**
     * The VM is configured to automatically run this class, and to call the
     * functions corresponding to each mode, as described in the IterativeRobot
     * documentation. 
     */
    public class Robot2015 : IterativeRobot
    {
        Drive mecDrive; 
        Compressor compressor;
        /**
         * This function is run when the robot is first started up and should be
         * used for any initialization code.
         */
        public override void RobotInit()
        {
            mecDrive = new Drive();
            compressor = new Compressor();

            compressor.Start();

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

        }

        /**
         * This function is called periodically during test mode
         */
        public override void TestPeriodic()
        {

        }

    }
}
