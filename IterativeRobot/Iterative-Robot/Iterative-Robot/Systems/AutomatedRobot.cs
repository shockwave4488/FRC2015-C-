using Iterative_Robot.Team_Code;

namespace Iterative_Robot.Systems
{
    public enum RobotStates { GetContainer, Stack, DropOff }

    //we'd need a limit switch on the back of the claw. I was told that wasn't hard.
    public class AutomatedRobot
    {
        private Stacker stacker;
        private SmartDrive drive;
        private int driveWait;
        private RobotStates state;
        private bool temp_HasContainer; //Replace with the limit switch output

        public AutomatedRobot()
        {
            stacker = new Stacker();
            drive = new SmartDrive();
            driveWait = 0;

            state = RobotStates.Stack; //Assuming we start with can by our corner.
        }

        public void Update()
        {
            switch (state)
            {
                case RobotStates.GetContainer:
                    drive.AlignGetContainer = true;
                    drive.DriveSpeeds = temp_HasContainer ? new point(0.5, 0) : new point(-0.5, 0);
                    driveWait = temp_HasContainer ? --driveWait : ++driveWait;
                    stacker.claw = temp_HasContainer;
                    if (temp_HasContainer) stacker.grabbingCan.ForceTrue(); else stacker.grabbingCan.ForceFalse();

                    if (temp_HasContainer && driveWait == 0)
                    {
                        //Clean up side effects
                        drive.AlignGetContainer = false;
                        drive.DriveSpeeds = new point(0, 0);
                        state = RobotStates.Stack;
                    }

                    break;
                case RobotStates.Stack:
                    stacker.state = StackerState.AlignTote;
                    drive.AlignLoad = true;
                    break;
            }
        }
    }
}