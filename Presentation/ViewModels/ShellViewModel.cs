using System.Windows.Input;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Common.MVVM;
using ResolutionsFlow.Infrastructure.Paths;
using ResolutionsFlow.Presentation.ViewModels.Pages;

namespace ResolutionsFlow.Presentation.ViewModels
{
    public sealed class ShellViewModel : ObservableObject
    {
        public INavigationService Navigation { get; }

        public string SettingsPath => AppPaths.SettingsFile;

        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateDocumentsCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateAboutCommand { get; }

        public ShellViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            NavigateDashboardCommand = new RelayCommand(() => Navigation.NavigateTo<DashboardViewModel>());
            NavigateDocumentsCommand = new RelayCommand(() => Navigation.NavigateTo<DocumentsViewModel>());
            NavigateSettingsCommand = new RelayCommand(() => Navigation.NavigateTo<SettingsViewModel>());
            NavigateAboutCommand = new RelayCommand(() => Navigation.NavigateTo<AboutViewModel>());
        }
    }
}
