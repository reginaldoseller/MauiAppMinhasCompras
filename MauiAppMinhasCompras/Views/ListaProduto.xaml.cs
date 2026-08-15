using MauiAppMinhasCompras.Models;
using System.Diagnostics;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    public ListaProduto()
    {
        InitializeComponent();
    }

    // Este método roda AUTOMATICAMENTE toda vez que a tela aparece
    // (inclusive quando você volta da tela de cadastro de NovoProduto!)
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarProdutos();
    }

    private async Task CarregarProdutos()
    {
        try
        {
            // Busca os produtos do SQLite usando o DbHelper
            List<Produto> lista = await App.DB.GetAll();

            // Entrega a lista para o CollectionView do XAML
            cvProdutos.ItemsSource = lista;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERRO AO BUSCAR PRODUTOS: {ex.Message}");
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // O seu método de navegação que já estava funcionando:
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}