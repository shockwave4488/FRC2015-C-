using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    class SWave_EdgeTrigger
    {
        private bool feedback;
        
        public bool CheckRising { get; set; }
        public bool CheckFalling { get; set; }

        public SWave_EdgeTrigger(bool checkRising, bool checkFalling)
        {
            feedback = false; CheckRising = checkRising; CheckFalling = checkFalling;
        }

        public void update(bool trigger)
        {
            feedback = trigger;
        }

        public bool Get(bool trigger)
        {
            return (CheckRising && (trigger && !feedback)) || (CheckFalling && (!trigger && feedback));
        }
    }
}
