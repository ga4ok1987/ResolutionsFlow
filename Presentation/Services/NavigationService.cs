using System;
using Microsoft.Extensions.DependencyInjection;
using ResolutionsFlow.Application.Interfaces;
using ResolutionsFlow.Common.MVVM;

namespace ResolutionsFlow.Presentation.Services
{
    public sealed class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _sp;

        private object _currentViewModel = null!;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        public NavigationService(IServiceProvider sp)
        {
            _sp = sp;
        }

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            CurrentViewModel = _sp.GetRequiredService<TViewModel>();
        }
    }
}
