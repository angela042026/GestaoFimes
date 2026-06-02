using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoFilmes.Domain.Entities
{
    internal class Realizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }

        public Realizador()
        {
            Nome = string.Empty;
            Pais = string.Empty;
        }
    }
}
