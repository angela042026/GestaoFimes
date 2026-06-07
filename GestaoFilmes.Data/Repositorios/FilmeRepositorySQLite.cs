using GestaoFilmes.Domain;
using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using GestaoFilmes.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GestaoFilmes.Data.Repositorios
{
    public class FilmeRepositorySQLite : IFilmeRepository
    {
        private readonly string _connectionString = "Data Source=filmes.db;Version=3;";

        public FilmeRepositorySQLite()
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS Filme (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Titulo TEXT NOT NULL,
                    Ano INTEGER,
                    Lingua TEXT,
                    Classificacao INTEGER,
                    CategoriaId INTEGER,
                    RealizadorId INTEGER
                );";

            using var cmd = new SQLiteCommand(createTableSql, conn);
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Filme filme)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = @"INSERT INTO Filme 
                           (Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId)
                           VALUES (@Titulo, @Ano, @Lingua, @Classificacao, @CategoriaId, @RealizadorId);";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
            cmd.Parameters.AddWithValue("@Ano", filme.Ano);
            cmd.Parameters.AddWithValue("@Lingua", filme.Lingua);
            cmd.Parameters.AddWithValue("@Classificacao", (int)filme.Classificacao);
            cmd.Parameters.AddWithValue("@CategoriaId", filme.CategoriaId);
            cmd.Parameters.AddWithValue("@RealizadorId", filme.RealizadorId);

            cmd.ExecuteNonQuery();
            filme.Id = (int)conn.LastInsertRowId;
        }

        public List<Filme> ObterTodos()
        {
            var lista = new List<Filme>();

            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Filme";

            using var cmd = new SQLiteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Filme
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = (ClassificacaoFilme)reader.GetInt32(4),
                    CategoriaId = reader.GetInt32(5),
                    RealizadorId = reader.GetInt32(6)
                });
            }

            return lista;
        }

        public Filme ProcurarPorTitulo(string titulo)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Filme WHERE LOWER(Titulo) = @Titulo";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Titulo", titulo.Trim().ToLower());

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Filme
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = (ClassificacaoFilme)reader.GetInt32(4),
                    CategoriaId = reader.GetInt32(5),
                    RealizadorId = reader.GetInt32(6)
                };
            }

            return null;
        }

        public bool Remover(int id)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM Filme WHERE Id = @Id";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}