using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using DesktopPet.Models;
using DesktopPet.ViewModels;
using System.Drawing;
using System.Timers;

namespace DesktopPet.Views;

public partial class MainWindow : Window
{
    MainViewModel viewModel;

    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;

        viewModel.Initialize(new int2
        {
            x = Screens.Primary.Bounds.Width,
            y = Screens.Primary.Bounds.Height
        }, MoveWindow, ResizeWindow);
        DataContext = viewModel;
    }

    private void MoveWindow(int2 pos)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            wind.Position = new PixelPoint(pos.x, pos.y);
        });
    }

    private void ResizeWindow(int2 size)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            wind.Width = size.x;
            wind.Height = size.y;
        });
    }

    private void GifImage_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            if (viewModel.Pause())
                this.BeginMoveDrag(e);
        }
    }

    private void GifImage_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        viewModel.SetWindowPosition(wind.Position.X, wind.Position.Y);
        viewModel.Resume();
    }

}