using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Iterative_Robot.Team_Code;

namespace Iterative_Robot.Systems
{
    public struct point
    {
        public double x, y;

        public point(double x_, double y_)
        {
            x = x_; y = y_;
        }

        public static point operator +(point a, point b)
        {
            return new point(a.x + b.x, a.y + b.y);
        }
        public static point operator -(point a)
        {
            return new point(-a.x, -a.y);
        }
    }

    public class SmartDrive
    {
        private Drive drive;
        private SWave_PID turnPID;
        private SWave_EdgeTrigger rotateTrigger;
        private SWave_AnalogueUltrasonic frontBack;
        private SWave_PID frontBackPID;
        private SWave_AnalogueUltrasonic side;
        private SWave_PID sidePID;
        private Gyro gyro;
        private SWave_Toggle fieldCentricToggle;

        public point DriveSpeeds { get; set; }
        public double TurnSetpoint { get { return turnPID.setpoint; } set { turnPID.setpoint = value; } }
        public double Rotation { get; set; }
        public bool FieldCentric { get { return fieldCentricToggle.state; } set { fieldCentricToggle.state = value; } }

        public bool AlignLoad { get; set; }
        public bool AlignNoodle { get; set; }
        public bool AlignOutput { get; set; }
        public bool AlignGetContainer { get; set; }

        public bool StrafeRightButton { get; set; }
        public bool StrafeForwardButton { get; set; }
        public bool StrafeBackButton { get; set; }
        public bool StrafeLeftButton { get; set; }

        public SmartDrive()
        {
            turnPID = new SWave_PID(Constants.Drive_TurnP, 0, Constants.Drive_TurnD, -0.5, 0.5);
            frontBackPID = new SWave_PID(Constants.Drive_AlignBackP, 0, Constants.Drive_AlignBackD, -0.25, 0.25);
            sidePID = new SWave_PID(Constants.Drive_AlignSideP, 0, Constants.Drive_AlignSideD, -0.5, 0.5);
            drive = new Drive();
            frontBack = new SWave_AnalogueUltrasonic(Constants.ChannelAnalogue_BackUltrasonic, Constants.UltraScaling);
            side = new SWave_AnalogueUltrasonic(Constants.ChannelAnalogue_SideUltrasonic, Constants.UltraScaling);
            gyro = new Gyro(Constants.ChannelAnalogue_Gyro);
            rotateTrigger = new SWave_EdgeTrigger(true, true);
            fieldCentricToggle = new SWave_Toggle();

            DriveSpeeds = new point(0, 0);
            TurnSetpoint = 0; Rotation = 0;
            FieldCentric = true; StrafeBackButton = false; StrafeLeftButton = false; StrafeForwardButton = false; StrafeRightButton = false;
            AlignNoodle = false; AlignLoad = false; AlignOutput = false;
        }

        private point fieldCentricAdj()
        {
            point toReturn;
            double angleRad = gyro.GetAngle() * Math.PI / 180.0;

            Console.WriteLine(DriveSpeeds.x);
            
            toReturn.x = (DriveSpeeds.x * Math.Cos(angleRad)) - (DriveSpeeds.y * Math.Sin(angleRad));
            toReturn.y = (DriveSpeeds.y * Math.Cos(angleRad)) + (DriveSpeeds.x * Math.Sin(angleRad));

            return toReturn;
        }

        public void update()
        {
            if (rotateTrigger.Get(Rotation == 0))
            {
                TurnSetpoint = gyro.GetAngle();
                WPILib.SmartDashboards.SmartDashboard.PutNumber("Heading set at", gyro.GetAngle());
                WPILib.SmartDashboards.SmartDashboard.PutString("ResetRotate", "Reset");
            }

            rotateTrigger.Update(Rotation == 0);

            if (AlignNoodle)
            {
                TurnSetpoint = Constants.Drive_AlignLoadAngle;
                frontBackPID.setpoint = Constants.Drive_AlignBackSetNoodle;
                DriveSpeeds = new point(0, frontBackPID.get(frontBack.get()));
            }
            else if (AlignOutput)
            {
                TurnSetpoint = 0;
                frontBackPID.setpoint = Constants.Drive_AlignBackSetOutput;
                DriveSpeeds = new point(0, frontBackPID.get(frontBack.get()));
            }
            else if (AlignLoad)
            {
                TurnSetpoint = Constants.Drive_AlignLoadAngle;
                frontBackPID.setpoint = Constants.Drive_AlignBackSetLoad;
                sidePID.setpoint = Constants.Drive_AlignSideSetpoint;
                DriveSpeeds = new point(sidePID.get(side.get()), frontBackPID.get(frontBack.get()));
            }

            if (Rotation == 0 || AlignOutput || AlignNoodle || AlignLoad)
                Rotation = turnPID.get(gyro.GetAngle());

            if (StrafeRightButton)
                DriveSpeeds = new point(Constants.Drive_StrafeButtonSpeed, 0);
            else if (StrafeLeftButton)
                DriveSpeeds = new point(-Constants.Drive_StrafeButtonSpeed, 0);
            else if (StrafeForwardButton)
                DriveSpeeds = new point(0, Constants.Drive_ForwardButtonSpeed);
            else if (StrafeBackButton)
                DriveSpeeds = new point(0, -Constants.Drive_ForwardButtonSpeed);

            drive.Rotation = Rotation;
            drive.X = FieldCentric ? fieldCentricAdj().x : DriveSpeeds.x;
            drive.Y = FieldCentric ? fieldCentricAdj().y : DriveSpeeds.y;
            drive.Update(null);

            turnPID.Update(gyro.GetAngle());
            sidePID.Update(side.get());
            frontBackPID.Update(frontBack.get());
        }

        public void resetGyro()
        {
            gyro.Reset();
            TurnSetpoint = 0;
        }

        public string writeEdge()
        {
            return rotateTrigger.Print() + "\n" + rotateTrigger.Get(Rotation == 0);
        }

        public string writeGyro()
        {
            return gyro.GetAngle().ToString();
        }
    }
}
