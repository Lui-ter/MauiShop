namespace petshop.Views;

public partial class About : FlyoutPage
{
    public About()
    {
        InitializeComponent();
    }

    private void OnHomeClicked(object? sender, TappedEventArgs e)
    {
        MostrarHome();
    }

    private void OnAboutClicked(object? sender, TappedEventArgs e)
    {
        MostrarAbout();
    }

    public void MostrarHome()
    {
        if (MainContainer.Current != null && Detail is NavigationPage nav && nav.CurrentPage is MainContainer)
        {
            MainContainer.Current.MostrarHome();
        }
        else
        {
            Detail = new NavigationPage(new MainContainer());
        }

        btnMenuHome.BackgroundColor = Color.FromArgb("#2A2A2A");
        btnMenuAbout.BackgroundColor = Colors.Transparent;
        IsPresented = false;
    }

    public void MostrarAbout()
    {
        if (MainContainer.Current != null && Detail is NavigationPage nav && nav.CurrentPage is MainContainer)
        {
            MainContainer.Current.MostrarAbout();
        }
        else
        {
            var mc = new MainContainer();
            Detail = new NavigationPage(mc);
            mc.MostrarAbout();
        }

        btnMenuHome.BackgroundColor = Colors.Transparent;
        btnMenuAbout.BackgroundColor = Color.FromArgb("#2A2A2A");
        IsPresented = false;
    }
}
