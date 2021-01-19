using System;

namespace ChallengeWebApp.Application
{
    public class TopicViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IdentityUserId { get; set; }
        public string IdentityUserName { get; set; }
        public string CreationDate { get; set; }
    }
}