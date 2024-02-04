using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections.ObjectModel;
using System.IO;

namespace DesktopPet.Models
{
    internal class ScenarioCollection
    {
        List<IScenarioPack> plugins = new List<IScenarioPack>();
        internal ScenarioCollection()
        {
            Load(new string[] { "Plugins" });
        }

        internal List<IScenarioPack> GetPacks()
        { return plugins; }

        void Load(string[] paths)
        {
            using (var _log = new StreamWriter("Log.txt"))
            {
                try
                {
                    plugins = new List<IScenarioPack>();
                    var catalog = new AggregateCatalog();
                    foreach (var path in paths)
                    {
                        catalog.Catalogs.Add(new DirectoryCatalog(path));
                        _log.WriteLine("Add catalog " + path);
                    }
                    _log.WriteLine("Create CompositionContainer");
                    var container = new CompositionContainer(catalog);
                    _log.WriteLine("Load plugins");
                    foreach (var lazyPlugin in container.GetExports<IScenarioPack>())
                    {
                        try
                        {
                            _log.WriteLine("Load plugin " + lazyPlugin.Value);
                            plugins.Add(lazyPlugin.Value);
                        }
                        catch (CompositionException ex)
                        {
                            _log.WriteLine(ex);
                        }
                    }
                }
                catch (Exception exc) { _log.WriteLine("Fatal: " + exc); }
            }
        }
    }
}
