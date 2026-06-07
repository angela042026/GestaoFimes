using GestaoFilmes.Domain.Entities;

using GestaoFilmes.Domain.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GestaoFilmes.Data.Repositorios
{
    public class RealizadorRepository : IRealizadorRepository
    {
        private readonly List<Realizador> _realizadores = new List<Realizador>();
        private int _idAtual = 1;

        public void Adicionar(Realizador realizador)
        {
            realizador.Id = _idAtual++;
            _realizadores.Add(realizador);
        }

        public List<Realizador> ObterTodos()
        {
            return _realizadores;
        }

        public Realizador ProcurarPorId(int id)
        {
            return _realizadores.FirstOrDefault(r => r.Id == id);
        }

        public bool Remover(int id)
        {
            var realizador = _realizadores.FirstOrDefault(r => r.Id == id);

            if (realizador == null)
                return false;

            _realizadores.Remove(realizador);
            return true;
        }
    }
}

