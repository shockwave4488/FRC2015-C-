using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot.SubSystems
{
    public enum ElevatorLocation { High, First_Tote, Bottom, Pickup, Stabilize }

    public class Elevator : Subsystem
    {
        private Talon ElevatorMotor;
        private AnalogPotentiometer pot;
        private Team_Code.SWave_PID PID;
        private Team_Code.SWave_AccelLimit limit;
        private ElevatorLocation setpoint_name;

        public bool pause { get; set; }
        public bool stop { get; set; }
        public ElevatorLocation loc
        {
            get { return setpoint_name; }
            set
            {
                if (!pause)
                    PID.setpoint = Constants.LiftLocations[value];
                setpoint_name = value;
            }
        }

        public Elevator()
        {
            pot = new AnalogPotentiometer(new AnalogInput(Constants.LiftPotChannel));
            ElevatorMotor = new Talon(Constants.LiftPort);
            PID = new Team_Code.SWave_PID(Constants.LiftP, 0, Constants.LiftD);
            pause = false; stop = false;
            limit = new Team_Code.SWave_AccelLimit(0.15);
        }

        public void update()
        {
            double value = PID.get(pot.Get());

            if (stop)
                value = 0;
            else if (pot.Get() > Constants.LiftLimitHigh)
                value = Math.Min(0, value);
            else if (pot.Get() < Constants.LiftLimitLow)
                value = Math.Max(0, value);

            ElevatorMotor.Set(value);
        }

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }
    }
}
