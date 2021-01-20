using Microsoft.AspNetCore.Identity;
using System;

namespace ChallengeWebApp.Models
{
    public class Topic
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string IdentityUserId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public IdentityUser IdentityUser { get; private set; }

        private Topic()
        { }

        public Topic(string title, string description, string identityUserId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IdentityUserId = identityUserId;
            CreationDate = DateTime.UtcNow;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}