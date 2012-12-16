using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BB.SmsQuiz.Model.Users;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BB.SmsQuiz.Repository.EF.Mapping
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("Users");
            HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("ID");
        }
    }
}
