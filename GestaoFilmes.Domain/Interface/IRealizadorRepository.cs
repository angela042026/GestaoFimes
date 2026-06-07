using GestaoFilmes.Domain.Entities;

using System.Collections.Generic;

namespace GestaoFilmes.Domain.Interface
{
    public interface IRealizadorRepository
    {
        void Adicionar(Realizador realizador);
        List<Realizador> ObterTodos();
        Realizador ProcurarPorId(int id);
        bool Remover(int id);
    }
}
