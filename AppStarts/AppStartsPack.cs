﻿using DesktopPet.Models;
using System.ComponentModel.Composition;

namespace AppStarts
{
    [Export(typeof(IScenarioPack))]
    public class AppStartsPack : IScenarioPack
    {
        public string Name => "Application start";

        public IScenario[] scenarios => new IScenario[]
        {
            new StartIt(new AppItem("VisualStudio", "avares://AppStarts/Images/VS.gif", 5)),
            new StartIt(new AppItem("Ubuntu", "avares://AppStarts/Images/Ubuntu_16.04_LTS_Starting.gif", 6)),
            new StartIt(new AppItem("Linux terminal", "avares://AppStarts/Images/LinuxTerminal.gif", 10)),
            new StartIt(new AppItem("windows xp", "avares://AppStarts/Images/windows-xp.gif", 5)),
            new StartIt(new AppItem("Logos", "avares://AppStarts/Images/LOGOS.gif", 5)),
            new StartIt(new AppItem("Logos settings", "avares://AppStarts/Images/LOGOS_settings.gif", 5))

        };
    }
}