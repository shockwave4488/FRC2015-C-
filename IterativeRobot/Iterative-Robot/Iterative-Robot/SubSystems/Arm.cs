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
                return (pot.Get() < PID.setpoint + Constants.ArmPosnTolerance) && (pot.Get() > PID.setpoint - Constants.ArmPosnTolerance);
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
            ArmMotor = new Talon(Constants.ArmPort);
            claw = new DoubleSolenoid(Constants.ClawChannel_Forward, Constants.ClawChannel_Reverse);
            pot = new AnalogPotentiometer(new AnalogInput(Constants.ArmPotChannel));
            limit = new Team_Code.SWave_AccelLimit(0.1);
            PID = new Team_Code.SWave_PID(Constants.ArmP, 0, Constants.ArmD);
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
                toReturn += /*"\n\tCurrently At " + pot.Get() + */"\n, Going To " + PID.setpoint + " (" + printSetpointName() + ")";
            else toReturn += /*"\n\tCurrently At " + pot.Get() + */"\n (" + printSetpointName() + ")";

            toReturn += "\n\t" + (Enabled ? "" : "Not ") + Enabled;

            return toReturn;
        }

        private string printSetpointName()
        {
            switch (setpoint_name)
            {
                case ArmLocation.Low:
                    return "Low";
                case ArmLocation.High:
                    return "High";
                case ArmLocation.Release:
                    return "Release";
                default:
                    return "ERROR : INVALID ARM SETPOINT NAME";
            }
        }
    }
}
