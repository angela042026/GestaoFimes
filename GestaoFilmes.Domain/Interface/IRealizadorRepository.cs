using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Entities.GestaoFilmes.Domain.Entities;
using System.Collections.Generic;

namespace GestaoFilmes.Domain.Interface
{
    public interface IRealizadorRepository
    {
        void Adicionar(Realizador realizador);
        List<Realizador> ObterTodos();
        Realizador ProcurarPorNome(string nome);
        Realizador ObterPorId(int id);
        bool Remover(int id);
    }
}