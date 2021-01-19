using ChallengeWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeWebApp.Data.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(topic => topic.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(topic => topic.Description)
                .IsRequired();

            builder.Property(topic => topic.CreationDate)
                .IsRequired();

            builder.HasOne(topic => topic.IdentityUser)
                .WithMany()
                .HasForeignKey(topic => topic.IdentityUserId)
                .IsRequired();
        }
    }
}