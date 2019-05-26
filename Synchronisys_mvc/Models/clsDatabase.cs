using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Synchronisys_mvc.Models
{
    public class clsDatabase : DbContext
    {

        public clsDatabase() : base("name=DBConnectionStr")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<clsDatabase>());
        }

        public DbSet<UserDetails> User { get; set; }
    }
}