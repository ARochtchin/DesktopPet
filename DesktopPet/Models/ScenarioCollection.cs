using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections.ObjectModel;

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
            try
            {
                plugins = new List<IScenarioPack>();
                var catalog = new AggregateCatalog();
                foreach (var path in paths)
                    catalog.Catalogs.Add(new DirectoryCatalog(path));
                var container = new CompositionContainer(catalog);
                foreach (var lazyPlugin in container.GetExports<IScenarioPack>())
                {
                    try
                    {
                        plugins.Add(lazyPlugin.Value);
                    }
                    catch (CompositionException ex)
                    {
                        //Console.WriteLine(ex);
                    }
                }
            }
            catch { }
        }
    }
}
