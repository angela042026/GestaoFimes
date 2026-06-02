using GestaoFilmes.Domain.Entities;
using GestaoFilmes.Domain.Interface;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data
{
    public class RealizadorRepository : IRealizadorRepository
    {
        private readonly List<Realizador> _realizadores = new List<Realizador>();
        private int _nextId = 1;

        public void Adicionar(Realizador realizador)
        {
            realizador.Id = _nextId++;
            _realizadores.Add(realizador);
        }

        public List<Realizador> ObterTodos()
        {
            return _realizadores;
        }

        public Realizador ProcurarPorRealizador(string nome)
        {
            return _realizadores.FirstOrDefault(r => r.Nome.ToLower() == nome.ToLower());
        }

        public void Remover(string nome)
        {
            var r = Procurar(nome);
            if (r != null)
                _realizadores.Remove(r);
        }
    }
}
