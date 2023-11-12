using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    public interface IScenarioPack
    {
        string Name { get; }
        IScenario[] scenarios { get; }
    }
}
