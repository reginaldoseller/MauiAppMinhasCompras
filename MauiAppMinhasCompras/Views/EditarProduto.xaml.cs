using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {

        try
        {
            Produto produto_anexado = BindingContext as Produto;

            if (produto_anexado == null)
            {
                await DisplayAlert("Erro", "Nenhum produto foi selecionado para edição.", "OK");
                return;
            }

            double.TryParse(txt_quantidade.Text, out double quantidade);
            double.TryParse(txt_preco.Text, out double preco);

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.DB.Update(p);
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            // Retorna para a página anterior
            await Navigation.PopModalAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }
}