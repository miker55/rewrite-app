using Microsoft.Extensions.DependencyInjection;

namespace Rewrite;

public partial class App : Application
{
    public App(IServiceProvider services)
    {
        InitializeComponent();

        Services = services;
    }

    public IServiceProvider Services { get; }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(Services.GetRequiredService<AppShell>());
    }
}
