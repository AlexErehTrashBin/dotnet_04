using Avalonia.Controls;
using Task04.ViewModels;

namespace Task04.Views;

public partial class ReflectionWindow : Window
{
    public ReflectionWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();
        DataContext = new ReflectionViewModel(mainViewModel);
    }

    private void OnSelectClass(object? sender, SelectionChangedEventArgs e)
    {
        (DataContext as ReflectionViewModel)?.OnSelectClass(sender, e);
    }

    private void OnSelectMethod(object? sender, SelectionChangedEventArgs e)
    {
        (DataContext as ReflectionViewModel)?.OnSelectMethod(sender, e);
    }
}