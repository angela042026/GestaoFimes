using GestaoFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoFilmes.Domain.Interface;


namespace GestaoFilmes.Domain.Interface
{
    public interface IRealizadorService
    {
        public void RegistrarRealizador(Realizador categoria);
        public List<Realizador> ListarRealizador();
        public void EliminarRealizador(string nome);
        public bool EliminarRealizador(int id);
    }
}
