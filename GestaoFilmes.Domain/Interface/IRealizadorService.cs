
using GestaoFilmes.Domain.Entities;

using System.Collections.Generic;

namespace GestaoFilmes.Domain.Interface
{
    public interface IRealizadorService
    {
        void RegistarRealizador(Realizador realizador);
        bool EliminarRealizador(int id);
        Realizador ProcurarRealizadorPorId(int id);
        List<Realizador> ListarRealizadores();
    }
}