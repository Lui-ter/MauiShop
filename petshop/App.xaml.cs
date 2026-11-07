namespace petshop;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // About is the application's FlyoutPage root. FlyoutPage and Shell
        // cannot be nested in the same visual tree.
        return new Window(new Views.About());
    }
}