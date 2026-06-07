using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GestaoFilmes.Data.Repositorios
{
    public class FilmeRepositorySQLite : IFilmeRepository
    {
        private readonly string _connectionString = "Data Source=filmes.db;Version=3;";

        // CONSTRUTOR: Garante que a tabela existe antes de qualquer operação!
        public FilmeRepositorySQLite()
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            // Cria a tabela Filme se ela não existir
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

        // Alterado de 'Adicionar' para 'Registar' (ajusta conforme a tua IFilmeRepository)
        public void Registar(Filme filme)
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

            // Captura o ID auto-incrementado gerado pelo SQLite
            filme.Id = (int)conn.LastInsertRowId;
        }

        // Alterado de 'ObterTodos' para 'Listar' (ajusta conforme a tua IFilmeRepository)
        public List<Filme> Listar()
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

        // Alterado de 'ProcurarPorTitulo' para 'ObterPorTitulo' (ajusta conforme a tua IFilmeRepository)
        public Filme ObterPorTitulo(string titulo)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Filme WHERE LOWER(Titulo) = @Titulo";

            using var cmd = new SQLiteCommand(sql, conn);
            // CORREÇÃO: Forçar o parâmetro a ir em minúsculas também
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

        // Alterado de 'Remover' para 'Eliminar' (ajusta conforme a tua IFilmeRepository)
        public bool Eliminar(int id)
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