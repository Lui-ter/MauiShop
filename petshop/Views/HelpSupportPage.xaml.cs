using petshop.DataAcces;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

public class HelpSupportData : BindingUtilObject
{
    public HelpSupportData()
    {
        var database = new ShopDbContext();
        Clients = new ObservableCollection<Client>(database.Clients);
    }

    public int _visitasPendientes;
    public int visitasPendientes
    {
        get { return _visitasPendientes; }
        set
        {
            if(_visitasPendientes != value)
            {
                _visitasPendientes = value;
                RaisePropertyChanged();
            }
        }
    }
    private ObservableCollection<Client> _clients;

    public ObservableCollection<Client> Clients
    {
        get { return _clients; }
        set
        {
            if (Clients != value)
            {
                _clients = value;
                RaisePropertyChanged();
            }
        }
    }
}
