using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MauiAppMinhasCompras.Models
{
    internal class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
    }
}
