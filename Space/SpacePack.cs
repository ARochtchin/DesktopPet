using DesktopPet.Models;
using System.ComponentModel.Composition;

namespace Space
{
    [Export(typeof(IScenarioPack))]
    public class SpacePack : IScenarioPack
    {
        public string Name => "Space";

        public IScenario[] scenarios => new IScenario[]
            {
                new Satellite()
            };
    }
}