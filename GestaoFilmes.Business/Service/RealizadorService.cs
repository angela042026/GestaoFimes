using GestaoFilmes.Domain.Entities;

using GestaoFilmes.Domain.Interface;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business
{
    public class RealizadorService : IRealizadorService
    {
        private readonly IRealizadorRepository _realizadorRepository;

        public RealizadorService(IRealizadorRepository realizadorRepository)
        {
            _realizadorRepository = realizadorRepository;
        }

        public void RegistarRealizador(Realizador realizador)
        {
            if (string.IsNullOrWhiteSpace(realizador.Nome))
                throw new Exception("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(realizador.Pais))
                throw new Exception("O país é obrigatório.");

            _realizadorRepository.Adicionar(realizador);
        }

        public bool EliminarRealizador(int id)
        {
            return _realizadorRepository.Remover(id);
        }

        public Realizador ProcurarRealizadorPorId(int id)
        {
            return _realizadorRepository.ProcurarPorId(id);
        }

        public List<Realizador> ListarRealizadores()
        {
            return _realizadorRepository.ObterTodos();
        }
    }
}
