﻿using System;
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
    public class Arm
    {
        private Talon ArmMotor;
        private DoubleSolenoid claw;
        private AnalogPotentiometer pot;
        private Team_Code.SWave_AccelLimit limit;
        private Team_Code.SWave_PID PID;
        private ArmLocation setpoint_name;

        public bool clawState { get; set; }
        public bool stop { get; set; }
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
        }

        public void update()
        {
            double value = PID.get(pot.Get());

            if (stop)
                value = 0;
            else if (pot.Get() > Constants.ArmLimitHigh)
                value = Math.Min(0, value);
            else if (pot.Get() > Constants.ArmLimitLow)
                value = Math.Max(0, value);

            ArmMotor.Set(value);
            claw.Set(clawState ? DoubleSolenoid.Value.Forward : DoubleSolenoid.Value.Reverse);
        }
    }
}
