using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterative_Robot.Team_Code
{
    /// <summary>
    /// Defines a standard interface for all classes.
    /// If a class explicitly defines any of these functions, it should probably implement this class.
    /// </summary>
    interface SW_IStandard<T>
    {
        string Name { get; set; }
        bool Enabled { get; set; }

        void Update(T Parameter);
        string ToString();
    }
}
