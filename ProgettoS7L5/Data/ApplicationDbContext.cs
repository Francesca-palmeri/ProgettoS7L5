using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoS7L5.Models;

namespace ProgettoS7L5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
         ApplicationUserRole, IdentityUserLogin<string>,
         IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
      
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<Artista> Artisti { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relazione Artista - Evento: Un Artista può essere associato a più Eventi
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Artista)  // Ogni Evento ha un Artista
                .WithMany(a => a.Eventi)  // Un Artista può avere molti Eventi
                .HasForeignKey(e => e.ArtistaId);  // La chiave esterna in Evento

            // Relazione Evento - Biglietto: Un Evento può avere più Biglietti
            modelBuilder.Entity<Biglietto>()
                .HasOne(b => b.Evento)  // Ogni Biglietto è associato a un Evento
                .WithMany(e => e.Biglietti)  // Un Evento può avere più Biglietti
                .HasForeignKey(b => b.EventoId);  // La chiave esterna in Biglietto

            // Relazione ApplicationUser - Biglietto: Un ApplicationUser può acquistare più Biglietti
            modelBuilder.Entity<Biglietto>()
                .HasOne(b => b.ApplicationUser)  // Ogni Biglietto è associato a un ApplicationUser
                .WithMany(u => u.Biglietti)  // Un ApplicationUser può avere più Biglietti
                .HasForeignKey(b => b.UserId);  // La chiave esterna in Biglietto

            modelBuilder.Entity<ApplicationUserRole>(userRole => {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.ApplicationUsers)
                    .HasForeignKey(ur => ur.RoleId);

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.ApplicationRoles)
                    .HasForeignKey(ur => ur.UserId);
            });

        }

    }
}
