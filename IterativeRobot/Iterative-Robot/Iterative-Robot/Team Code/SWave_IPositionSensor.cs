using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Interface for wrapping sensors that can track position, used in PIDSubsystem class.
    /// </summary>
    public interface SWave_IPositionSensor
    {
        double get();
    }
}
