using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using PROJECTSOLUTION.Models;

namespace PROJECTSOLUTION.DATA
{
    public class LetsEmployeeContext : DbContext
    {
        public LetsEmployeeContext() : base("DefaultConnection") =>
        //Database.SetInitializer(new DefaultConnectionInitializer());
        Database.SetInitializer<LetsEmployeeContext>(new CreateDatabaseIfNotExists<LetsEmployeeContext>());
        public DbSet<USER_ACCOUNT> USER_ACCOUNTs { get; set; }
    






    }
}
