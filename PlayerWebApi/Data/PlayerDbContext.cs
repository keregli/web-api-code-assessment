// /////////////////////////////////////////////////////////////////////////////
// PLEASE DO NOT RENAME OR REMOVE ANY OF THE CODE BELOW. 
// YOU CAN ADD YOUR CODE TO THIS FILE TO EXTEND THE FEATURES TO USE THEM IN YOUR WORK.
// /////////////////////////////////////////////////////////////////////////////
using Microsoft.EntityFrameworkCore;
using PlayerWebApi.Data.Entities;

namespace PlayerWebApi.Data;

public class PlayerDbContext : DbContext
{
    public PlayerDbContext()
        : base() { }

    public PlayerDbContext(DbContextOptions<PlayerDbContext> options) 
        : base(options) { }

    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerSkill> PlayerSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>(config => config.HasKey(ps => ps.Id));

        modelBuilder.Entity<PlayerSkill>(config =>
        {
            config.HasKey(ps => ps.Id);
            config.HasOne<Player>().WithMany(p => p.PlayerSkills).HasForeignKey(ps => ps.PlayerId);
        });
    }
}