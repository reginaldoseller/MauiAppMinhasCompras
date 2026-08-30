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

        try
        {
            //limpando a lista para não duplicar os produtos
            lista.Clear();

            List<Produto> tap = await App.DB.GetAll();
            tap.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // método para navegar para a tela de cadastro de novo produto:
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


    // método para filtrar os produtos em tempo real
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {

        try
        {

            string q = e.NewTextValue;

            lista.Clear();

            List<Produto> tap = await App.DB.Search(q);

            tap.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }


    // método para somar os produtos
    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            double soma = lista.Sum(i => i.Total);

            string msg = $"O valor total dos produtos é: {soma:C}";

            DisplayAlert("Total", msg, "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // método que remove o produto
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {

        try
        {
            MenuItem selecionado = sender as MenuItem;
            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert("Tem Certeza", "Remover Produto", "Sim", "Não");

            if(confirm)
            {
                await App.DB.Delete(p.Id);
                //retira também da ObservableCollection
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
           await DisplayAlert("Ops", ex.Message, "OK");
        }

    }


    // método para selecionar o produto e enviá-lo para a edição (aqui é utilizado o BindingContext para que a nova tela de edição conheça a lista de produtos)
    private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            await Navigation.PushModalAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }
}