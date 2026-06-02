using GestaoFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoFilmes.Domain.Interface
{
        public interface IRealizador
        {
            public void Adicionar(Realizador realizador);
            public List<Filme> ObterTodos();
            public Filme ProcurarPorRealizador(string nome);
            public bool Remover(int id);
        }
    
}
