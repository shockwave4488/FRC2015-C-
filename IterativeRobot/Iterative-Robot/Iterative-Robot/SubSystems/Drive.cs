using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace Iterative_Robot
{
    /// <summary>
    /// All controls necessary for Mechanum Drive
    /// </summary>
    public class Drive : Team_Code.SWave_IStandard<object>
    {
        private Talon LF, RF, LR, RR;
        private Team_Code.SWave_AccelLimit limitX, limitY, limitR;
        private Team_Code.SWave_Toggle FieldCentricToggle;

        public bool FieldCentric { get { return FieldCentricToggle.state; } set { FieldCentricToggle.state = value; } }
        public string Name { get { return ""; } set { } }
        public bool Enabled { get; set; }
        public double X { get; set; } //Sideways
        public double Y { get; set; } //Forward
        public double Rotation { get; set; }

        public Drive()
        {
            LF = new Talon(Constants.DriveLFPort);
            RF = new Talon(Constants.DriveRFPort);
            LR = new Talon(Constants.DriveLRPort);
            RR = new Talon(Constants.DriveRRPort);

            X = 0; Y = 0;

            limitX = new Team_Code.SWave_AccelLimit(Constants.DriveAccelLimit);
            limitY = new Team_Code.SWave_AccelLimit(Constants.DriveAccelLimit);
            limitR = new Team_Code.SWave_AccelLimit(Constants.DriveAccelLimit);

            RF.Inverted = true; RR.Inverted = true;

            Enabled = true;
        }

        public void Update(object UNUSED)
        {
            limitX.Update(X); limitY.Update(Y); limitR.Update(Rotation);

            double[] toReturn = new double[4];
            toReturn[0] = limitY.Get() - limitX.Get() - limitR.Get(); //LF
            toReturn[1] = limitY.Get() + limitX.Get() + limitR.Get(); //RF
            toReturn[2] = limitY.Get() + limitX.Get() - limitR.Get(); //LR
            toReturn[3] = limitY.Get() - limitX.Get() + limitR.Get(); //RR

            //normalize values
            double max = toReturn.Max();
            if (max > 1)
                for (int i = 0; i < toReturn.Length; i++)
                    toReturn[i] /= max;

            if (Enabled)
            {
                LF.Set(toReturn[0]);
                RF.Set(toReturn[1]);
                LR.Set(toReturn[2]);
                RR.Set(toReturn[3]);
            }
        }

        public string Print()
        {
            string toReturn = Name + " " + GetType();

            toReturn += "\n\tX : " + X + "\n\tY : " + Y + "\n\tR : " + Rotation;
            toReturn += "\n\t" + (FieldCentric ? "" : "Not ") + "Field Centric Control";
            toReturn += "\n\t" + (Enabled ? "" : "Not ") + "Enabled";

            return toReturn;
        }
    }
}