using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}


	// método para enviar para o bando os dados do formulário ao clicar
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text)
			};

			await App.DB.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

		} catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}

    }
}