using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using back_end.Entidades;

namespace back_end.Repositorios
{
    public class RepositorioEnMemoria: IRepositorio
    {
        private List<Genero> _generos;

        //public RepositorioEnMemoria()
        public RepositorioEnMemoria(ILogger<RepositorioEnMemoria> logger)
        {
            _generos = new List<Genero>()
            {
                new Genero() { Id = 1, Nombre = "Drama" },
                new Genero() { Id = 2, Nombre = "Comedia"},
                new Genero() { Id = 3, Nombre = "Acción"},
                new Genero() { Id = 4, Nombre = "Suspenso"}
            };

            _guid = Guid.NewGuid();
        }

        public Guid _guid;

        public List<Genero> ObtenerTodosLosGeneros()
        {
            return _generos;
        }

        public async Task<Genero> ObtenerPorId(int Id)
        {
            await Task.Delay(1);
            return _generos.FirstOrDefault(x => x.Id == Id);
        }

        public Guid ObtenerGuid()
        {
            return _guid;
        }

        public void CrearGenero(Genero genero)
        {
            genero.Id = _generos.Count() + 1;
            _generos.Add(genero);

        }
    }
}
