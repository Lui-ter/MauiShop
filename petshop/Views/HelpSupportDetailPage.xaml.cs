namespace petshop.Views;

public partial class HelpSupportDetailPage : ContentPage, IQueryAttributable
{
    public HelpSupportDetailPage()
    {
        InitializeComponent();
    }

    public HelpSupportDetailPage(int clientId) : this()
    {
        ApplyQueryAttributes(new Dictionary<string, object> { ["id"] = clientId });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Title = $"Cliente: {query["id"]}";
    }
}