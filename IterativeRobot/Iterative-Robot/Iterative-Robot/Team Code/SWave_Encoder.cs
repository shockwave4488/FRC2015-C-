using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPILib;

namespace Iterative_Robot.Team_Code
{
    class SWave_Encoder : Encoder, SWave_IPositionSensor
    {
        public SWave_Encoder(DigitalSource Asource, DigitalSource Bsource) : base(Asource, Bsource) { }
        public SWave_Encoder(int Achannel, int Bchannel) : base(Achannel, Bchannel) { }
        public SWave_Encoder(DigitalSource Asource, DigitalSource Bsource, bool Reverse) : base(Asource, Bsource, Reverse) { }
        public SWave_Encoder(int Achannel, int Bchannel, bool Reverse) : base(Achannel, Bchannel, Reverse) { }
        public SWave_Encoder(DigitalSource Asource, DigitalSource Bsource, DigitalSource Indexsource) : base(Asource, Bsource, Indexsource) { }
        public SWave_Encoder(int Achannel, int Bchannel, int IndexChannel) : base(Achannel, Bchannel, IndexChannel) { }

        public double get()
        {
            return base.Get();
        }
    }
}
