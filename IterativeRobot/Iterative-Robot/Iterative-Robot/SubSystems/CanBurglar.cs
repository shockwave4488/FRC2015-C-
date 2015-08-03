using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.SubSystems
{
    public class CanBurglar : Subsystem
    {
        private DoubleSolenoid grabber;
        private DigitalInput HallEffect;

        public bool grab { get; set; }
        public bool grabbed { get; set; }

        public CanBurglar()
        {
            grabber = new DoubleSolenoid(Constants.Can_Burglar_Forward, Constants.Can_Burglar_Reverse);
            HallEffect = new DigitalInput(Constants.HallEffectChannel);
        }

        public void update()
        {
            grabber.Set(grab ? DoubleSolenoid.Value.Forward : DoubleSolenoid.Value.Reverse);
            grabbed = HallEffect.Get();
        }

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }
    }
}
