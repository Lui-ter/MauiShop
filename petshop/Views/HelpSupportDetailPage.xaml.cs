using petshop.ViewModels;

namespace petshop.Views;

public partial class HelpSupportDetailPage : ContentPage
{
    public HelpSupportDetailPage(HelpSupportDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}