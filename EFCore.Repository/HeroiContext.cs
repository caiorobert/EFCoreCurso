using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Repository
{
	public class HeroiContext : DbContext
	{
		//public HeroiContext()
		//{
				
		//}
		public HeroiContext(DbContextOptions<HeroiContext> options) : base(options) {}
		public DbSet<Heroi> Herois { get; set; }
		public DbSet<Batalha> Batalhas { get; set; }
		public DbSet<Arma> Armas { get; set; }
		public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }
		public DbSet<IdentidadeSecreta> IdentidadesSecretas { get; set; }

		//Caso queira deixar a configuração do banco de dados aqui, descomendar as linhas abaixo. Se não, só usar o HeroiContext DbContextOptions acima
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Password=123Aa321;Persist Security Info=True;User ID=sa;Initial Catalog=HeroApp;Data Source=LAPTOP-46VKID3V\\SQLEXPRESS");
		//}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<HeroiBatalha>(entity => 
			{
				entity.HasKey(e => new { e.BatalhaId, e.HeroiId });
			});
		}
	}
}
