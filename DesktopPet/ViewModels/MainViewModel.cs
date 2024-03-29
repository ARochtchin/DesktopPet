﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using DesktopPet.Models;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Timers;

namespace DesktopPet.ViewModels;

public class MainViewModel : ViewModelBase
{
    ScenarioManager scenarioManager;

    public MainViewModel()
    {
        scenarioManager = new ScenarioManager();
        Packs = new ObservableCollection<PackViewModel>(Array.ConvertAll(scenarioManager.GetPacks(), x => new PackViewModel(x)));
        ShowSettings = ReactiveCommand.Create(showSettings);
        Hide = ReactiveCommand.Create(() => { if (scenarioManager != null) scenarioManager.Sleep(); });
        Show = ReactiveCommand.Create(() => { if (scenarioManager != null) scenarioManager.ShowRandom(); });
    }

    internal void Initialize(int2 screenSize, Action<int2> moveWindow, Action<int2> resizeWindow)
    {
        scenarioManager.SetScreenSize(screenSize);
        scenarioManager.eActiveChange += (scenario) => { CurrentScenarioName = scenario.Title; };
        scenarioManager.eMove += moveWindow;
        scenarioManager.eResize += resizeWindow;
        scenarioManager.eGIF_Refresh += (path) => { ActiveGif = path; };
        scenarioManager.Sleep();
    }

    public bool Pause()
    {
        if (scenarioManager != null) 
            return scenarioManager.Pause(true);
        return false;
    }

    public bool Resume()
    {
        if (scenarioManager != null)
            return scenarioManager.Pause(false);
        return false;
    }

    string _activeGIF = "avares://DesktopPet/Images/None.gif";
    public string ActiveGif
    {
        get => _activeGIF;
        private set { _activeGIF = value; this.RaisePropertyChanged(); }
    }

    public ObservableCollection<PackViewModel> Packs { get; private set; }

    string _currentScenarioName = "";
    public String CurrentScenarioName
    {
        get => _currentScenarioName;
        set { _currentScenarioName = value; this.RaisePropertyChanged(); }
    }

    Object _selected;
    public object Selected
    {
        get { return _selected; }
        set
        {
            _selected = value; this.RaisePropertyChanged();
            if(value is PackViewModel)
            {
            }
            else if(value is ScenarioViewModel)
            {
                var selected = value as ScenarioViewModel;
                scenarioManager.current = selected.Model.Scenario;
            }
        }
    }

    public void SetWindowPosition(int x, int y)
    {
        if (scenarioManager != null)
            scenarioManager.SetWindowPosition(new int2 { x = x, y = y });
    }

    public int SleepTime
    {
        get { return scenarioManager.SleepTime; }
        set { scenarioManager.SleepTime = value; this.RaisePropertyChanged(); }
    }

    public ReactiveCommand<Unit, Unit> ShowSettings { get; }
    private void showSettings()
    {
        var settings = new Settings();
        settings.DataContext = this;
        settings.Show();
    }

    public ReactiveCommand<Unit, Unit> Hide { get; }
    public ReactiveCommand<Unit, Unit> Show { get; }
}





