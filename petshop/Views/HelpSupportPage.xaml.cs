namespace petshop.Views;

public partial class HelpSupportPage : ContentPage
{
    public HelpSupportPage()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var dataObject = Resources["data"] as HelpSupportData;
        dataObject.visitasPendientes = 30;
    }
}

public class HelpSupportData
{
    public int visitasPendientes { get; set; }
}
