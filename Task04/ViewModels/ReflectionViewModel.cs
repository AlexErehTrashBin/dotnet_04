using System.Reflection;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;
using Task04.Models;

namespace Task04.ViewModels;

public class ReflectionViewModel(MainViewModel mainViewModel) : ViewModelBase
{
    private List<Type> _classes = [];
    private Type? _selectedClass;
    private MethodInfo? _selectedMethod;
    private object? _instance;

    public ICommand LoadAssemblyCommand => ReactiveCommand.Create<string>(LoadAssembly);
    public ICommand ExecuteMethodCommand => ReactiveCommand.Create(ExecuteMethod);
    public ICommand LoadMethodsCommand => ReactiveCommand.Create<string>(LoadMethods);

    public List<Type> Classes
    {
        get => _classes;
        set => this.RaiseAndSetIfChanged(ref _classes, value);
    }

    private Type? SelectedClass
    {
        get => _selectedClass;
        set => this.RaiseAndSetIfChanged(ref _selectedClass, value);
    }

    public List<MethodInfo>? SelectedClassMethods =>
        SelectedClass?.GetMethods()
            .Where(m => m is
            {
                IsSpecialName: false,
                IsStatic: false,
                IsConstructor: false,
                IsGenericMethod: false
            })
            .ToList();

    public string? SelectedClassName => _selectedClass?.Name;

    private MethodInfo? SelectedMethod
    {
        get => _selectedMethod;
        set => this.RaiseAndSetIfChanged(ref _selectedMethod, value);
    }

    public string? SelectedMethodName => _selectedMethod?.Name;

    public void OnSelectClass(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count <= 0)
        {
            return;
        }

        var selectedClassName = e.AddedItems[0]!.ToString();
        SelectedClass = _classes.FirstOrDefault(c => c.FullName == selectedClassName);
    }

    public void OnSelectMethod(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count <= 0)
        {
            return;
        }

        var selectedMethodName = e.AddedItems[0]!.ToString();
        var selectedMethod = SelectedClassMethods!.FirstOrDefault(c => c.ToString() == selectedMethodName);
        SelectedMethod = selectedMethod;
    }

    private void LoadAssembly(string path)
    {
        var assembly = Assembly.LoadFrom(path);
        Classes = assembly.GetTypes()
            .Where(t =>
                t.GetTypeInfo().BaseType == typeof(LightingFixture) ||
                t.GetTypeInfo().ImplementedInterfaces.Any(i => i == typeof(IPluggable))
            )
            .ToList();
    }

    private void LoadMethods(string selectedClassName)
    {
        if (SelectedClass == null) return;
        _instance = SelectedClass.Name switch
        {
            "Chandelier" => mainViewModel.Chandelier,
            "DeskLamp" => mainViewModel.DeskLamp,
            "FloorLamp" => mainViewModel.FloorLamp,
            "Lantern" => mainViewModel.Lantern,
            _ => _instance
        };
        this.RaisePropertyChanged(nameof(SelectedClassMethods));
        if (SelectedClassMethods is not { Count: > 0 }) return;
        var method = SelectedClass.GetMethod(SelectedClassMethods[0].Name);
        SelectedMethod = method;
        this.RaisePropertyChanged(nameof(SelectedMethodName));
    }

    private void ExecuteMethod()
    {
        if (_instance == null || SelectedMethod == null) return;
        SelectedMethod.Invoke(_instance, null);
    }
}