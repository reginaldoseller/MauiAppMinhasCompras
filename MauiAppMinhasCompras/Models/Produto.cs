using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        // O PrimaryKey e AutoIncrement vêm do SWLite - são anotations
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string? Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
    }
}
