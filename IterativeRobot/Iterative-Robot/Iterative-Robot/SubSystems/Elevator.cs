using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.SubSystems
{
    /// <summary>
    /// Options for the Elevator Location
    /// </summary>
    public enum ElevatorLocation { High, First_Tote, Bottom, Pickup, Stabilize }

    /// <summary>
    /// Elevator Subsystem
    /// </summary>
    public class Elevator : Team_Code.SWave_IStandard<double>
    {
        private Talon ElevatorMotor;
        private AnalogPotentiometer pot;
        private Team_Code.SWave_PID PID;
        private Team_Code.SWave_AccelLimit limit;
        private ElevatorLocation setpointName;

        public string Name { get { return ""; } set { } }
        public bool pause { get; set; }
        public bool Enabled { get; set; }
        
        public bool AtPosition
        {
            get
            {
                return (pot.Get() < PID.setpoint + Constants.Lift_PosnTolerance) &&
                    (pot.Get() > PID.setpoint - Constants.Lift_PosnTolerance);
            }
        }
        public ElevatorLocation loc
        {
            get { return setpointName; }
            set
            {
                if (!pause)
                    PID.setpoint = Constants.Lift_Locations[value];
                setpointName = value;
            }
        }

        public Elevator()
        {
            pot = new AnalogPotentiometer(new AnalogInput(Constants.ChannelAnalogue_LiftPot));
            ElevatorMotor = new Talon(Constants.ChannelPWM_Lift);
            PID = new Team_Code.SWave_PID(Constants.Lift_P, 0, Constants.Lift_D);
            pause = false; Enabled = true;
            limit = new Team_Code.SWave_AccelLimit(0.15, true, false);
            loc = ElevatorLocation.Bottom;
        }

        public void Update(double manual)
        {
            limit.Update(PID.get(pot.Get()));
            double value = limit.Get();

            if (!Enabled)
                value = manual;
            else if (pot.Get() > Constants.Lift_LimitHigh)
                value = Math.Min(0, value);
            else if (pot.Get() < Constants.Lift_LimitLow)
                value = Math.Max(0, value);

            ElevatorMotor.Set(-value);
        }

        public string Print()
        {
            string toReturn = Name + GetType();

            if (!AtPosition)
                toReturn += "\n\tCurrently At " + pot.Get() + ", Going To " + PID.setpoint + " (" + setpointName.ToString() + ")";
            else
                toReturn += "\n\tCurrently At " + pot.Get() + " (" + setpointName.ToString() + ")";

            toReturn += "\n\t" + (Enabled ? "" : "Not ") + "Enabled";
            return toReturn;
        }
    }
}
