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
    /// </summary>
    public class SWave_PIDSubsystem : Subsystem
    {
        private SWave_PID PID;
        private SWave_IPositionSensor sensor;
        private PWMSpeedController Motor;
        private double setpoint_;
        
        public double setpoint
        {
            get { return setpoint_; }
            set
            {
                setpoint_ = value;
                PID.setpoint = value;
            }
        }

        public SWave_PIDSubsystem(PWMSpeedController Motor_, SWave_IPositionSensor sensor_, double kP, double kI, double kD)
        {
            sensor = sensor_; Motor = Motor_;
            PID = new SWave_PID(kP, kI, kD);
        }

        public virtual void update()
        {
            PID.update(sensor.get());
            Motor.Set(PID.get(sensor.get()));
        }

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }
    }
}
