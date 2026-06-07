using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System.Collections.Generic;
using System.Data.SQLite;



namespace GestaoFilmes.Data.Repositorios
{
    public class RealizadorRepositorySQLite : IRealizadorRepository
    {
        private readonly string _connectionString = "Data Source=filmes.db";

        public void Adicionar(Realizador realizador)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "INSERT INTO Realizador (Nome, Pais) VALUES (@Nome, @Pais)";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Nome", realizador.Nome);
            cmd.Parameters.AddWithValue("@Pais", realizador.Pais);

            cmd.ExecuteNonQuery();
            realizador.Id = (int)conn.LastInsertRowId;
        }

        public List<Realizador> ObterTodos()
        {
            var lista = new List<Realizador>();

            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT * FROM Realizador";

            using var cmd = new SQLiteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Realizador
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Pais = reader.GetString(2)
                });
            }

            return lista;
        }

        public Realizador ProcurarPorId(int id)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT * FROM Realizador WHERE Id = @Id";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Realizador
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Pais = reader.GetString(2)
                };
            }

            return null;
        }

        public bool Remover(int id)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM Realizador WHERE Id = @Id";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
