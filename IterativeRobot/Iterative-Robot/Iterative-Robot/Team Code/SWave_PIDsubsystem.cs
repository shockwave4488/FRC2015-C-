using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Extendable controller for a PID Controlled mechanism.
    /// Requires SWave_IPositionSensor implementing sensor.
    /// Defines No Constructors.
    /// </summary>
    public abstract class SWave_PIDSubsystem : SWave_IStandard<object>
    {
        protected SWave_PID PID;
        protected SWave_IPositionSensor sensor;
        protected PWMSpeedController Motor;
        protected double tolerance;
        protected int setpointName_;

        public string Name { get; set; }
        public bool Enabled { get; set; }
        
        public bool AtSetpoint
        {
            get { return (sensor.get() < PID.setpoint + tolerance) && (sensor.get() > PID.setpoint - tolerance); }
        }
        public int setpointName
        {
            get { return setpointName_; }
            set { PID.setpoint = value; }
        }

        public virtual void Update(object UNUSED)
        {
            PID.Update(sensor.get());
            Motor.Set(PID.get(sensor.get()));
        }

        public virtual string Print()
        {
            string toReturn = Name + " " + GetType();

            if (!AtSetpoint)
                toReturn += "\n\tCurrently At" + sensor.get() + ", Going To " + PID.setpoint + " (" + printSetpointName() + ")";
            else
                toReturn += "\n\tCurrently At" + sensor.get() + ", Going To " + PID.setpoint + " (" + printSetpointName() + ")";

            toReturn += "\n\t" + (Enabled ? "" : "Not ") + "Enabled";
            return toReturn;
        }

        protected abstract string printSetpointName();
    }
}
