namespace petshop.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private void Menu_Tapped(object? sender, TappedEventArgs e)
    {
        if (Application.Current?.Windows.FirstOrDefault()?.Page is FlyoutPage flyout)
        {
            flyout.IsPresented = !flyout.IsPresented;
        }
    }

    private void IrAHome_Clicked(object? sender, EventArgs e)
    {
        if (Application.Current?.Windows.FirstOrDefault()?.Page is About flyout)
        {
            flyout.MostrarHome();
        }
        else if (Application.Current?.Windows.FirstOrDefault()?.Page is FlyoutPage fp)
        {
            fp.Detail = new NavigationPage(new MainContainer());
            fp.IsPresented = false;
        }
    }
}

