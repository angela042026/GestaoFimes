using System;

namespace GestaoFilmes.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Construtor para garantir a segurança contra valores nulos
        public Categoria()
        {
            Nome = string.Empty;
        }
    }
}
