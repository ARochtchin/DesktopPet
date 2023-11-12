using DesktopPet.Models;
using System.ComponentModel.Composition;

namespace WinnieThePooh
{
    [Export(typeof(IScenarioPack))]
    public class WinnieThePoohPack : IScenarioPack
    {
        private readonly IScenario[] _scenarios;

        public WinnieThePoohPack()
        {
            _scenarios = new IScenario[] {
                new Sighs(true),
                new Sighs(false),
                new Stay(),
                new Move(false,false),
                new Move(true,true),
                new Move(false,true),
                new Move(true,false),
                new UpBush()
            };
        }

        public string Name => "Winnie-the-Pooh";

        public IScenario[] scenarios => _scenarios;
    }
}
