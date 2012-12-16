using System.Data.Entity.ModelConfiguration;
using BB.SmsQuiz.Model.Competitions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BB.SmsQuiz.Repository.EF.Mapping
{
    public class CompetionMapper : EntityTypeConfiguration<Competition>
    {
        public CompetionMapper()
        {
            ToTable("Competitions");
            HasKey(t => t.ID);
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("ID");

            this.Property(t => t.ClosingDate)
                .HasColumnName("ClosingDate");

            this.Property(t => t.CompetitionKey)
                .HasColumnName("CompetitionKey");

            this.Property(t => t.CreatedDate)
                .HasColumnName("CreatedDate");

            this.Property(t => t.Question)
                .HasColumnName("Question");

            this.Property(t => t.State.Status)
                .HasColumnName("Status");

            this.Property(t => t.Winner.ID)
                .HasColumnName("WinnerID");
        }
    }
}
