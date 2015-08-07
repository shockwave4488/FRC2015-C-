using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;

namespace Iterative_Robot.SubSystems
{
    /// <summary>
    /// The states that the conveyor can report
    /// </summary>
    public enum conveyorState { Done, In, Out, Invalid }

    /// <summary>
    /// Subsystem including ramp, conveyor, and tote stopper
    /// </summary>
    public class ToteChute : Team_Code.SWave_IStandard<object>
    {
        private DoubleSolenoid ramp;
        private DoubleSolenoid stop;
        private Talon conveyor;
        private DigitalInput backBeam;
        private DigitalInput frontBeam;

        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool Stopper { get; set; }
        public bool RampIn { get; set; }
        public bool output { get; set; }

        public ToteChute()
        {
            ramp = new DoubleSolenoid(Constants.ChannelSolenoid_RampF, Constants.ChannelSolenoid_RampR);
            stop = new DoubleSolenoid(Constants.ChannelSolenoid_StopF, Constants.ChannelSolenoid_StopR);
            conveyor = new Talon(Constants.ChannelPWM_Output);
            backBeam = new DigitalInput(Constants.ChannelDIO_BeamBack);
            frontBeam = new DigitalInput(Constants.ChannelDIO_BeamFront);
            Name = ""; Enabled = true;
        }

        public void Update(object UNUSED)
        {
            if (output)
                conveyor.Set(Math.Abs(Constants.Conveyor_OutputSpeed)); //Replace with OutSpeed
            else if (getState() == conveyorState.In)
                conveyor.Set(-Math.Abs(Constants.Conveyor_InSpeed));
            else if (getState() == conveyorState.Out)
                conveyor.Set(Math.Abs(Constants.Conveyor_OutSpeed));
            else if (getState() == conveyorState.Done)
                conveyor.Set(0);

            stop.Set(Stopper ? DoubleSolenoid.Value.Reverse : DoubleSolenoid.Value.Forward);
            ramp.Set(RampIn ? DoubleSolenoid.Value.Reverse : DoubleSolenoid.Value.Forward);
        }

        public conveyorState getState()
        {
            //True == Broken
            if (!backBeam.Get() && !frontBeam.Get())
                return conveyorState.Out;
            if (!frontBeam.Get() && backBeam.Get())
                return conveyorState.Done;
            if (frontBeam.Get() && backBeam.Get())
                return conveyorState.In;
            return conveyorState.Invalid;
        }

        public static string PrintConveyorState(conveyorState state)
        {
            switch (state)
            {
                case conveyorState.Done:
                    return "Done";
                case conveyorState.In:
                    return "In";
                case conveyorState.Out:
                    return "Out";
                case conveyorState.Invalid:
                    return "Invalid";
                default:
                    return "NOT A VALID STATE";
            }
        }

        public string Print()
        {
            string toReturn = Name + " " + GetType();

            toReturn += "\n\tCurrent Tote State: " + PrintConveyorState(getState());
            toReturn += "\n\tBack Beam Break: " + backBeam.Get();
            toReturn += "\n\tFront Beam Break: " + frontBeam.Get();

            return toReturn;
        }
    }
}
