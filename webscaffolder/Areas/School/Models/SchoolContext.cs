namespace webscaffolder.Areas.School.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SchoolContext : DbContext
    {
        // Your context has been configured to use a 'SchoolContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'webscaffolder.Areas.School.Models.SchoolContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SchoolContext' 
        // connection string in the application configuration file.
        public SchoolContext()
            : base("name=DefaultConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<School> Schools { get; set; }
         public virtual DbSet<EductionOffice> EductionOffices { get; set; }
         public virtual DbSet<SchoolGender> SchoolGenders { get; set; }
         public virtual DbSet<SchoolLevel> SchoolLevels { get; set; }
         public virtual DbSet<SchoolType> SchoolTypes { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}