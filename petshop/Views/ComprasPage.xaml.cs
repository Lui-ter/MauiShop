using petshop.ViewModels;

namespace petshop.Views;

public partial class ComprasPage : ContentPage
{
    public ComprasPage(ComprasViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
