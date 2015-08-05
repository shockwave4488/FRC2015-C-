using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Iterative_Robot.SubSystems;

namespace Iterative_Robot.State_Machines
{
    public enum StackerState { Nothing, Wait, LiftDown, LiftUp, AlignTote, Paused }

    public class Stacker
    {
        private Team_Code.SWave_WaitByCallCount alignWait;
        private Team_Code.SWave_WaitByCallCount liftUpWait;
        private Team_Code.SWave_WaitByCallCount ReleaseWait;
        private Elevator elevator;
        private Arm arm;
        private ToteChute toteChute;
        private StackerState stateLast;
        private StackerState state_;
        private bool stacking;

        public int toteCount { get; private set; }
        public StackerState state
        {
            get { return state_; }
            set
            {
                if (value == StackerState.Paused && state_ != StackerState.Paused)
                    stateLast = state_;
                state_ = value;
            }
        }

        public Stacker()
        {
            elevator = new Elevator(); toteChute = new ToteChute(); arm = new Arm();
            alignWait = new Team_Code.SWave_WaitByCallCount(7);
            liftUpWait = new Team_Code.SWave_WaitByCallCount(10);
            ReleaseWait = new Team_Code.SWave_WaitByCallCount(10);
        }

        public void Update()
        {
            switch (state)
            {
                case StackerState.Nothing:
                    if (toteCount > 0)
                        elevator.loc = ElevatorLocation.Stabilize;
                    else
                        elevator.loc = ElevatorLocation.Bottom;

                    toteChute.Stopper = false;
                    toteChute.RampIn = true;
                    break;

                case StackerState.Wait: //Wait for the lift to go Up.
                    liftUpWait.Update(true);
                    if (liftUpWait.WaitComplete && elevator.AtPosition)
                        state = StackerState.AlignTote;
                    break;

                case StackerState.LiftDown:
                    alignWait.Update(false); //reset alignWait before AlignTote.

                    if(toteCount >= 5)
                    {
                        elevator.loc = ElevatorLocation.Stabilize;
                        state = StackerState.LiftDown;
                    }
                    else
                    {
                        elevator.loc = ElevatorLocation.Pickup;
                        state = elevator.AtPosition ? StackerState.LiftUp : StackerState.LiftDown;
                    }
                    break;

                case StackerState.LiftUp:
                    elevator.loc = ElevatorLocation.High;
                    liftUpWait.Update(false); //reset liftUpWait for the wait state.
                    toteCount++;
                    state = StackerState.Wait;
                    break;

                case StackerState.AlignTote:
                    if (alignWait.WaitComplete)
                    {
                        state = StackerState.LiftDown;
                        break;
                    }
                    arm.loc = toteCount >= 2 && ReleaseWait.WaitComplete ? ArmLocation.Release : ArmLocation.High;
                    ReleaseWait.Update(toteCount == 2);
                    alignWait.Update(toteChute.getState() == conveyorState.Done);

                    break;

                case StackerState.Paused:
                    elevator.pause = true;
                    break;

                default:
                    break;
            }
        }

        private int DBToteCount()
        {
            int temp = (int)WPILib.SmartDashboards.SmartDashboard.GetNumber("Tote Count");
            return (temp == 0 ? 6 : temp); //If dashboard disconnects for some reason, assume we want a stack of six
        }
    }
}
