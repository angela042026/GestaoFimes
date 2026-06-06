using GestaoFilmes.Data.Repositorios;
using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Entities.GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System.Collections.Generic;

namespace GestaoFilmes.Data.Repositorios
{
    public class RealizadorRepository : IRealizadorRepository
    {
        public void Adicionar(Realizador realizador) { }
        public List<Realizador> ObterTodos() => new();
        public Realizador ProcurarPorNome(string nome) => null;
        public Realizador ObterPorId(int id) => null;
        public bool Remover(int id) => false;
    }
}

