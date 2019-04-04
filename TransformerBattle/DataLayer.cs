using System;
using Microsoft.EntityFrameworkCore;

namespace TransformerBattle.DataLayer
{
    public class TransformerContext : DbContext
    {
        public TransformerContext(DbContextOptions<TransformerContext> options) : base(options) { }

        public DbSet<Transformer> Transformers { get; set; }
        public DbSet<TransformerScore> TransformerScore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transformer>().ToTable("Transformer");
        }
    }

    public class Transformer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Group Allegiance { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Speed { get; set; }
        public int Endurance { get; set; }
        public int Rank { get; set; }
        public int Courage { get; set; }
        public int Firepower { get; set; }
        public int Skill { get; set; }

        public int GetOverAll()
        {
            return Strength + Intelligence + Speed + Endurance + Rank + Courage + Firepower + Skill;
        }
    }

    public class TransformerScore
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
    }

    public enum Group
    {
        Autobot = 1,
        Decepticon = 2
    }
}