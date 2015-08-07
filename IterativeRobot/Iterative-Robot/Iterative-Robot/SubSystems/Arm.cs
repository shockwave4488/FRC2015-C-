using System;
using System.Collections.Generic;
using System.Linq;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.SubSystems
{
    /// <summary>
    /// Options for the location of the arm
    /// </summary>
    public enum ArmLocation { Low, High, Release } 

    /// <summary>
    /// Can-Grabbing Arm System
    /// </summary>
    public class Arm : Team_Code.SWave_IStandard<double>
    {
        private Talon ArmMotor;
        private DoubleSolenoid claw;
        private AnalogPotentiometer pot;
        private Team_Code.SWave_AccelLimit limit;
        private Team_Code.SWave_PID PID;
        private ArmLocation setpoint_name;

        public string Name { get { return ""; } set { } }
        public bool clawState { get; set; }
        public bool Enabled { get; set; }

        public bool AtPosition
        {
            get
            {
                return (pot.Get() < PID.setpoint + Constants.Arm_PosnTolerance) && (pot.Get() > PID.setpoint - Constants.Arm_PosnTolerance);
            }
        }
        public ArmLocation loc
        {
            get { return setpoint_name; }
            set
            {
                setpoint_name = value;
                PID.setpoint = Constants.ArmLocations[value];
            }
        }

        public Arm()
        {
            ArmMotor = new Talon(Constants.ChannelPWM_Arm);
            claw = new DoubleSolenoid(Constants.ChannelSolenoid_ClawF, Constants.ChannelSolenoid_ClawR);
            pot = new AnalogPotentiometer(new AnalogInput(Constants.ChannelAnalogue_ArmPot));
            limit = new Team_Code.SWave_AccelLimit(0.1);
            PID = new Team_Code.SWave_PID(Constants.Arm_P, 0, Constants.Arm_D);
            Enabled = true;
            loc = ArmLocation.High;
        }

        public void Update(double manual)
        {
            limit.Update(PID.get(pot.Get()));
            double value = limit.Get();

            if (!Enabled)
                value = manual;
            /*
            else if (pot.Get() > Constants.ArmLimitHigh)
                value = Math.Min(0, value);
            else if (pot.Get() > Constants.ArmLimitLow)
                value = Math.Max(0, value);
                */

            ArmMotor.Set(Math.Max(-0.7, Math.Min(0.4, value)));
            claw.Set(clawState ? DoubleSolenoid.Value.Reverse : DoubleSolenoid.Value.Forward);
        }

        public string Print()
        {
            string toReturn = Name + GetType();

            if (!AtPosition)
                toReturn += /*"\n\tCurrently At " + pot.Get() + */"\n, Going To " + PID.setpoint + " (" + setpoint_name.ToString() + ")";
            else toReturn += /*"\n\tCurrently At " + pot.Get() + */"\n (" + setpoint_name.ToString() + ")";

            toReturn += "\n\t" + (Enabled ? "" : "Not ") + Enabled;

            return toReturn;
        }
    }
}
