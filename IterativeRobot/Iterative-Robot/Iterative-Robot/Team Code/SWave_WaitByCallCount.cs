using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    public class SWave_WaitByCallCount : SWave_IStandard<bool>
    {
        private int count;
        private int threshold;

        public bool Enabled { get; set; }
        public string Name { get; set; }

        public SWave_WaitByCallCount(int count_)
        {
            threshold = count_;
            Name = "Anonymous";
            Enabled = true;
        }

        public bool WaitComplete
        {
            get { return count > threshold; }
        }

        public void Update(bool increase)
        {
            count = (increase && Enabled) ? count + 1 : 0;
        }

        public string Print()
        {
            string toReturn = Name + " " + GetType();
            toReturn += "\n\tCurrent Count : " + count + ", Waiting For " + threshold;
            return toReturn;
        }
    }
}
