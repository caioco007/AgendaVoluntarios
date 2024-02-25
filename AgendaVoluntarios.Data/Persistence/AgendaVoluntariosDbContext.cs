using AgendaVoluntarios.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Persistence
{
    public class AgendaVoluntariosDbContext : DbContext
    {
        public AgendaVoluntariosDbContext(DbContextOptions<AgendaVoluntariosDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileEvent> ProfileEvent { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventMusic> EventMusic { get; set; }
        public DbSet<Music> Music { get; set; }
        public DbSet<Function> Function { get; set; }
        public DbSet<ProfileFunction> ProfileFunction { get; set; }
    }
}
