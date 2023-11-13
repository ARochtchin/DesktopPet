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
                new Satellite(),
                new Alien(new AlienItem("Alien 1", "avares://Space/Images/Alien1.gif", 8)),
                new Alien(new AlienItem("Alien 2", "avares://Space/Images/Alien2.gif", 12)),
                new Landing()
            };
    }
}