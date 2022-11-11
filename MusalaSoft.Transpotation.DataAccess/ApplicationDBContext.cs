using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MusalaSoft.Transpotation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        private static DbContextOptions _options;

        public ApplicationDBContext() : base(_options)
        { 
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
            _options = options;
        }

        public DbSet<DroneStateDataModel> DroneState { get; set; }

        public DbSet<DroneModelDataModel> DroneModel { get; set; }

        public DbSet<DroneDataModel> Drone { get; set; }

        public DbSet<DroneTripDataModel> DroneTrip { get; set; }

        public DbSet<MedicationDataModel> Medication { get; set; }
    }
}
