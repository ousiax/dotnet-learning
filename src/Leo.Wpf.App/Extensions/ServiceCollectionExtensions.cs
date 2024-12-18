﻿// MIT License

using System.Reflection;
using CommunityToolkit.Mvvm.Messaging;
using Leo.Wpf.App;
using Leo.Wpf.App.Services;
using Leo.Wpf.App.ViewModels;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLeoViewModels(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMessenger, WeakReferenceMessenger>();

            services.AddTransient<IMainWindowService, MainWindowService>();
            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<CustomerViewModel>();

            services.AddTransient<NewCustomerViewModel>();
            services.AddTransient<INewCustomerWindowService, NewCustomerWindowService>();

            services.AddTransient<FindCustomerViewModel>();
            services.AddTransient<IFindWindowService, FindWindowService>();

            services.AddTransient<NewCustomerDetailViewModel>();
            services.AddTransient<INewCustomerDetailWindowService, NewCustomerDetailWindowService>();

            services.AddTransient<CustomerEditorViewModel>();
            services.AddTransient<ICustomerEditorWindowService, CustomerEditorWindowService>();

            services.AddTransient<EchoViewModel>();
            services.AddTransient<IEchoWindowService, EchoWindowService>();

            return services;
        }
    }
}
