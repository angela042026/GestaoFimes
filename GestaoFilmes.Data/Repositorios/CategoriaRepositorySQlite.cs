using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System.Collections.Generic;
using System.Data.SQLite;


namespace GestaoFilmes.Data.Repositorios
{
    public class CategoriaRepositorySQLite : ICategoriaRepository
    {
        private readonly string _connectionString = "Data Source=filmes.db";

        public void Adicionar(Categoria categoria)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "INSERT INTO Categoria (Nome) VALUES (@Nome)";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Nome", categoria.Nome);

            cmd.ExecuteNonQuery();
            categoria.Id = (int)conn.LastInsertRowId;
        }

        public List<Categoria> ObterTodos()
        {
            var lista = new List<Categoria>();

            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT * FROM Categoria";

            using var cmd = new SQLiteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Categoria
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                });
            }

            return lista;
        }

        public Categoria ProcurarPorId(int id)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT * FROM Categoria WHERE Id = @Id";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Categoria
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                };
            }

            return null;
        }

        public Categoria ProcurarPorNome(string nome)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT * FROM Categoria WHERE LOWER(Nome) = @Nome";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Nome", nome.ToLower());

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Categoria
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                };
            }

            return null;
        }

        public bool Remover(int id)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM Categoria WHERE Id = @Id";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
