using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace ResolutionsFlow.Presentation.Views;

public partial class ShellWindow : FluentWindow
{
    public ShellWindow()
    {
        InitializeComponent();

        RootNavigation.SelectionChanged += RootNavigation_SelectionChanged;

        // стартова сторінка
        Navigate("Dashboard");
    }

    private void RootNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is not NavigationViewItem item || item.Tag is not string tag)
            return;

        Navigate(tag);
    }

    private void Navigate(string tag)
    {
        // Тут заміни на свої сторінки/вʼюшки.
        // Мінімальний варіант — Page. MVVM підʼєднаємо далі (через DI/NavigationService).
        Page page = tag switch
        {
            "Dashboard" => new Views.DashboardPage(),
            "Documents" => new Views.DocumentsPage(),
            _ => new Views.DashboardPage()
        };

        MainFrame.Navigate(page);
    }
}
