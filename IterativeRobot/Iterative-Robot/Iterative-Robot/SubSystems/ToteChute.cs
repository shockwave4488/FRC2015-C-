using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.SubSystems
{
    /// <summary>
    /// The states that the conveyor can report
    /// </summary>
    public enum conveyorState { Done, In, Out, invalid}

    /// <summary>
    /// Subsystem including ramp, conveyor, and tote stopper
    /// </summary>
    public class ToteChute : Subsystem
    {
        private DoubleSolenoid ramp;
        private DoubleSolenoid stop;
        private Talon conveyor;
        private DigitalInput backBeam;
        private DigitalInput frontBeam;

        public bool stacking { get; set; }
        public bool output { get; set; }

        public ToteChute()
        {
            ramp = new DoubleSolenoid(Constants.RampChannel_Forward, Constants.RampChannel_Reverse);
            stop = new DoubleSolenoid(Constants.StopChannel_Forward, Constants.StopChannel_Reverse);
            conveyor = new Talon(Constants.ConveyorPort);
            backBeam = new DigitalInput(Constants.BeamBackChannel);
            frontBeam = new DigitalInput(Constants.BeamFrontChannel);
        }

        public void update()
        {
            if (output)
                conveyor.Set(0.5); //Replace with OutSpeed
            else if (getState() == conveyorState.In)
                conveyor.Set(-0.1);
            else if (getState() == conveyorState.Out)
                conveyor.Set(0.1);

            stop.Set(stacking ? DoubleSolenoid.Value.Forward : DoubleSolenoid.Value.Reverse);
        }

        private conveyorState getState()
        {
            if (backBeam.Get() && frontBeam.Get())
                return conveyorState.Out;
            if (frontBeam.Get() && !backBeam.Get())
                return conveyorState.Done;
            if (!frontBeam.Get() && !backBeam.Get())
                return conveyorState.In;
            return conveyorState.invalid;
        }

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }
    }
}
