using DesktopPet.Models;
using System.ComponentModel.Composition;

namespace AppStarts
{
    [Export(typeof(IScenarioPack))]
    public class AppStartsPack : IScenarioPack
    {
        public string Name => "Application start";

        public IScenario[] scenarios => new IScenario[]
        {
            new StartIt(new AppItem("VisualStudio", "avares://AppStarts/Images/VS.gif", new int2(680,422)))
        };
    }
}