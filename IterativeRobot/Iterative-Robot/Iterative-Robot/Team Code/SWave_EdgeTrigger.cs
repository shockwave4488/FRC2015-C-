using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    public class SWave_EdgeTrigger : SWave_IStandard<bool>
    {
        private bool feedback;
        
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public bool CheckRising { get; set; }
        public bool CheckFalling { get; set; }

        public SWave_EdgeTrigger(bool checkRising, bool checkFalling)
        {
            feedback = false; CheckRising = checkRising; CheckFalling = checkFalling; Name = "Anonymous"; Enabled = true;
        }

        public void Update(bool trigger)
        {
            feedback = trigger;
        }

        public bool Get(bool trigger)
        {
            return Enabled && ((CheckRising && (trigger && !feedback)) || (CheckFalling && (!trigger && feedback)));
        }

        public string Print()
        {
            string toReturn = Name + " " + GetType();
            toReturn += "\n\tFeedback: " + feedback;

            return toReturn;
        }
    }
}
