using DesktopPet.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.ViewModels
{
    class ScenarioModel
    {
        internal IScenario Scenario { get; private set; }
        internal bool IsUsed { get; set; }

        internal ScenarioModel(IScenario scenario, PackModel pacl)
        {
            Scenario = scenario;
            Pack = pacl;
            IsUsed = true;
        }
        internal PackModel Pack { get; private set; }
    }

    public class ScenarioViewModel : ViewModelBase
    {
        ScenarioModel _scenario;
        internal ScenarioViewModel(ScenarioModel scenario)
        {
            _scenario = scenario;
        }

        public String Title => _scenario.Scenario.Title;

        public bool IsUsed
        {
            get => _scenario.IsUsed;
            set { _scenario.IsUsed = value; this.RaisePropertyChanged(); }
        }

        internal ScenarioModel Model => _scenario;
        internal PackModel Pack => _scenario.Pack;
    }


    class PackModel
    {
        IScenarioPack _pack;
        ScenarioModel[] _scenarios;
        internal PackModel(IScenarioPack pack)
        {
            _pack = pack;
            _scenarios = Array.ConvertAll(_pack.scenarios, x => new ScenarioModel(x, this));
            IsUsed = true;
        }

        internal string Name => _pack.Name;
        internal ScenarioModel[] Scenarios => _scenarios;

        internal bool IsUsed { get; set; }
    }

    public class PackViewModel : ViewModelBase
    {
        PackModel _pack;
        internal PackViewModel(PackModel pack)
        {
            _pack = pack;
            Scenarios = new ObservableCollection<ScenarioViewModel>(Array.ConvertAll(pack.Scenarios, x => new ScenarioViewModel(x)));
        }

        public String Title => _pack.Name;
        public ObservableCollection<ScenarioViewModel> Scenarios { get; private set; }
        public bool IsUsed
        {
            get => _pack.IsUsed;
            set { _pack.IsUsed = value; this.RaisePropertyChanged(); }
        }
    }
}
