using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot
{
    public class Drive : Subsystem
    {
        private Talon LF, RF, LR, RR;
        private Team_Code.SWave_AccelLimit limitX, limitY, limitR;
        private double _X, _Y, _R;

        public double X { get { return _X; } set { _X = limitX.update(value); } } //Sideways
        public double Y { get { return _Y; } set { _Y = limitY.update(value); } } //Forward
        public double Rotation { get { return _R; } set { _R = limitR.update(value); } }

        public Drive()
        {
            LF = new Talon(Constants.DriveLFPort);
            RF = new Talon(Constants.DriveRFPort);
            LR = new Talon(Constants.DriveLRPort);
            RR = new Talon(Constants.DriveRRPort);

            limitX = new Team_Code.SWave_AccelLimit(Constants.DriveAccelLimit);
            limitY = new Team_Code.SWave_AccelLimit(Constants.DriveAccelLimit);
            limitR = new Team_Code.SWave_AccelLimit(Constants.DriveAccelLimit);
        }

        public void update()
        {
            double[] toReturn = new double[4];
            toReturn[0] = Y - X - Rotation; //LF
            toReturn[1] = Y + X + Rotation; //RF
            toReturn[2] = Y + X - Rotation; //LR
            toReturn[3] = Y - X + Rotation; //RR

            //normalize values
            double max = toReturn.Max();
            if (max > 1)
                for (int i = 0; i < toReturn.Length; i++)
                    toReturn[i] /= max;

            LF.Set(toReturn[0]);
            RF.Set(toReturn[1]);
            LR.Set(toReturn[2]);
            RR.Set(toReturn[3]);
        }

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }
    }
}