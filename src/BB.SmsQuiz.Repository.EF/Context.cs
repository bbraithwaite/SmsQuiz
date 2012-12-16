using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Repository.EF
{
    public class Context : DbContext
    {
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<User> Users { get; set; }

        public Context() 
            : base("SmsQuizConnection")
        {
            Database.SetInitializer<Context>(null); 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // custom mappers
            modelBuilder.Configurations.Add(new Mapping.CompetionMapper());
            modelBuilder.Configurations.Add(new Mapping.UserMapper());
        }
    }
}
