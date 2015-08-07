using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Iterative_Robot.SubSystems;

namespace Iterative_Robot.Systems
{
    public enum StackerState { Nothing, Wait, LiftDown, LiftUp, AlignTote, Paused }

    /// <summary>
    /// Encapsulates everything but the drive train and can grabber, manages stacking and can pickup.
    /// </summary>
    public class Stacker : Team_Code.SWave_IStandard<object>
    {
        private Team_Code.SWave_WaitByCallCount alignWait;
        private Team_Code.SWave_WaitByCallCount liftUpWait;
        private Team_Code.SWave_WaitByCallCount ReleaseWait;
        public Team_Code.SWave_Toggle grabbingCan;
        private Elevator elevator;
        private Arm arm;
        private ToteChute toteChute;
        private StackerState stateLast;
        private StackerState state_;

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int toteCount { get; private set; }
        public bool output { get; set; }
        public bool ArmDown { get { return grabbingCan.state; } set { grabbingCan.state = value; } }
        public bool claw { get; set; }
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

        private Notifier notifier;

        public Stacker()
        {
            elevator = new Elevator(); toteChute = new ToteChute(); arm = new Arm();
            alignWait = new Team_Code.SWave_WaitByCallCount(7);
            liftUpWait = new Team_Code.SWave_WaitByCallCount(10);
            ReleaseWait = new Team_Code.SWave_WaitByCallCount(10);
            grabbingCan = new Team_Code.SWave_Toggle();
            state = StackerState.Nothing;
            ArmDown = false; output = false; claw = true; toteCount = 0;


            notifier = new Notifier(() =>
            {
                Update(null);
            });
        }

        public void Start()
        {
            notifier.StartPeriodic(0.01);
        }

        public void Update(object UNUSED)
        {
            toteChute.output = output;

            if (output) { elevator.loc = ElevatorLocation.Stabilize; toteChute.Stopper = false; }
            else
                switch (state)
                {
                    case StackerState.Nothing:
                        if (toteCount > 0)
                        {
                            elevator.loc = ElevatorLocation.Stabilize;
                        }
                        else
                        {
                            elevator.loc = ElevatorLocation.Bottom;
                            arm.loc = ArmDown ? ArmLocation.Low : ArmLocation.High;
                            arm.clawState = claw;
                        }
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
                        if (toteCount >= 5)
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
                        if (toteCount == 0)
                            elevator.loc = ElevatorLocation.First_Tote;
                        arm.loc = (toteCount >= 2 && ReleaseWait.WaitComplete) || arm.loc == ArmLocation.Release ? ArmLocation.Release : ArmLocation.High;
                        arm.clawState = toteCount >= 2;
                        ReleaseWait.Update(toteCount == 2);
                        alignWait.Update(toteChute.getState() == conveyorState.Done);
                        toteChute.Stopper = true;
                        toteChute.RampIn = false;
                        break;

                    case StackerState.Paused:
                        elevator.pause = true;
                        break;

                    default:
                        break;
                }

            arm.Update(0.0);
            elevator.Update(0.0);
            toteChute.Update(null);
        }

        public void Reset()
        {
            toteCount = 0;
            state = StackerState.Nothing;
        }

        private int DBToteCount()
        {
            int temp = (int)WPILib.SmartDashboards.SmartDashboard.GetNumber("Tote Count");
            return (temp == 0 ? 6 : temp); //If dashboard disconnects for some reason, assume we want a stack of six
        }
        
        public string Print()
        {
            string toReturn = elevator.Print() + "\n" + toteChute.Print() + "\n" + arm.Print();
            toReturn += "\n\tState: " + state.ToString();
            toReturn += "\n\t" + toteCount + " Totes";

            return toReturn;
        }
    }
}
