using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace Iterative_Robot.Team_Code
{
    public class NavX
    {
        private I2C I2CRef;
        private SPI SPIRef;
        private int updateRate;

        public int UpdateRate { get { return updateRate; } set { updateRate = Math.Max(Math.Min(value, 60), 4); } }

        public NavX(I2C.Port bus, int UpdateRate_)
        {
            I2CRef = new I2C(bus, 32); UpdateRate = UpdateRate_;
        }
        public NavX(SPI.Port bus, int UpdateRate_)
        {
            SPIRef = new SPI(bus); UpdateRate = UpdateRate_;
        }
    }
}
