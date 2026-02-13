using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ResolutionsFlow.Presentation.ViewModels;
using Wpf.Ui.Controls;

namespace ResolutionsFlow.Presentation.Views;

public partial class ShellWindow : FluentWindow
{
    private readonly ShellViewModel _vm;

    public ShellWindow(ShellViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;

        _vm = vm;

        Loaded += ShellWindow_Loaded;
    }

    private void ShellWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var host = new ContentControl();

        BindingOperations.SetBinding(
            host,
            ContentControl.ContentProperty,
            new Binding("Navigation.CurrentViewModel"));

        RootNavigation.ReplaceContent(host);

        // стартова сторінка
        _vm.NavigateDashboardCommand.Execute(null);
    }
}
