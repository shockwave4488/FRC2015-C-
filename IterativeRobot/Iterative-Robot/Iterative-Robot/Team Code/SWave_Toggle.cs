using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    class SWave_Toggle
    {
        private bool _state;
        private bool feedback;

        public SWave_Toggle() { feedback = false; _state = false; }

        public bool state
        {
            get
            {
                return _state;
            }
            set
            {
                if (value && !feedback)
                    _state = !_state;

                feedback = value;
            }
        }
    }
}
