using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.SubSystems
{
    /// <summary>
    /// Can-Burglar Subsystem
    /// </summary>
    public class CanBurglar : Team_Code.SWave_IStandard<object>
    {
        private DoubleSolenoid grabber;
        private DigitalInput HallEffect;

        public string Name { get { return ""; } set { } }
        public bool Enabled { get; set; }

        public bool grab { get; set; }
        public bool grabbed { get; set; }

        public CanBurglar()
        {
            grabber = new DoubleSolenoid(Constants.Can_Burglar_Forward, Constants.Can_Burglar_Reverse);
            HallEffect = new DigitalInput(Constants.HallEffectChannel);
            Enabled = true;
        }

        public void Update(object UNUSED)
        {
            if(Enabled)
                grabber.Set(grab ? DoubleSolenoid.Value.Forward : DoubleSolenoid.Value.Reverse);
            grabbed = HallEffect.Get();
        }

        public string Print()
        {
            string toReturn = Name + GetType();

            toReturn += "\n\t" + (grab ? "" : "Not ") + "Actuated";
            toReturn += "\n\t" + (grabbed ? "" : "Not ") + "Grabbed";
            toReturn += "\n\t" + (Enabled ? "" : "Not ") + "Enabled";

            return toReturn;
        }
    }
}
