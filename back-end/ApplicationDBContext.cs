using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using back_end.Entidades;

using Microsoft.EntityFrameworkCore;


namespace back_end
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Genero> Generos { set; get; }
    }
}
