using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        //########################################
        //parte da conexão
        //tipo somente leitura
        //essa propriedade vai armazenar a "conexão"
        readonly SQLiteAsyncConnection _conn;

        //apontando onde está o arquivo
        public SQLiteDatabaseHelper(string caminhoDoArquivo)
        {
            //recebe um novo objeto com a coneção com o caminho
            _conn = new SQLiteAsyncConnection(caminhoDoArquivo);
            //cria a tabela produto se ela não existir
            _conn.CreateTableAsync<Produto>().Wait();
        }



        // #########################################
        //daqui pra frente foram declarados todos os métodos
        //p é uma variável de entrada do tipo Produto
        //Task é a tarefa que está retornando
        public Task<int> Insert(Produto p)
        {
            // chama a propriedade conexao e insere de form assincrona
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            //sql com os marcadores
            // Esse código dá para simplificar
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? Where Id=?";

            // A ordem dos parâmetros tém que ser a mesma da cláusula sql
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
                );
        }

        public Task<int> Delete(int id)
        {
            //Preciso mostrar de qual tabela vai deletar
            //exprexão Lâmbida (i => i.Id == id) lemos todos os itens da tabela onde a id seja igual a id
            // Nesse caso o id é um parâmetro e o Id está na model Produto
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        //listar os produtos. Pega todos
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        //Procura por um termo de pesquisa
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }

    }
}
