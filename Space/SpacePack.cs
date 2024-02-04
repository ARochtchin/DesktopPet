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
                new Booster(),
                new StaticGif(new StaticItem("Alien 1", "avares://Space/Images/Alien1.gif", 8)),
                new StaticGif(new StaticItem("Alien 2", "avares://Space/Images/Alien2.gif", 12)),
                new StaticGif(new StaticItem("Astronaut", "avares://Space/Images/Astronaut.gif", 5)),
                new StaticGif(new StaticItem("Astronaut mind blowing", "avares://Space/Images/AstronautMindBlowing.gif", 3)),
                new Landing(),
                new UFO(),
                new Rocket(),
                new MoonSatellite(),
                new SpaceShip(),
                new SatelliteSpace(),
            };
    }
}