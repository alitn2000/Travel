using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities.CheckListManagement;
using Travel.Domain.Core.Entities.TripManagement;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.InfraStructure.EfCore.Configurations;

namespace Travel.InfraStructure.EfCore.Common;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TripConfiguration());
        modelBuilder.ApplyConfiguration(new CheckListConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<CheckList> CheckLists { get; set; }
    public DbSet<CheckListTrip> CheckListTrips { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<UserTrip> UserTrips { get; set; }
}
