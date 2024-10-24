using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LigaTaller.Models;

namespace LigaTaller.Data
{
    public class LigaTallerContext : DbContext
    {
        public LigaTallerContext (DbContextOptions<LigaTallerContext> options)
            : base(options)
        {
        }

        public DbSet<LigaTaller.Models.Equipo> Equipo { get; set; } = default!;
        public DbSet<LigaTaller.Models.Jugadores> Jugadores { get; set; } = default!;
        public DbSet<LigaTaller.Models.Estadio> Estadio { get; set; } = default!;
    }
}
