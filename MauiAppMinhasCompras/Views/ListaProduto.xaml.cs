using MauiAppMinhasCompras.Models;
using System.Diagnostics;
using System.Collections.ObjectModel; // <-- ADICIONAR

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // Criando o objeto ObservableCollection
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    // Toda vez que a tela carregar é executada a busca no SQLite e abastece a lita de produtos.
    protected async override void OnAppearing()
    {
        List<Produto> tap = await App.DB.GetAll();
        tap.ForEach(i => lista.Add(i));
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

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tap = await App.DB.Search(q);

        tap.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O valor total dos produtos é: {soma:C}";

        DisplayAlert("Total", msg, "Ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}