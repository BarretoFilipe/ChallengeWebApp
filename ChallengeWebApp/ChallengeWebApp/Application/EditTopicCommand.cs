using System;
using System.ComponentModel.DataAnnotations;

namespace ChallengeWebApp.Application
{
    public class EditTopicCommand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "'Title' must not be empty.")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Maximum 150 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "'Description' must not be empty.")]
        public string Description { get; set; }
        public string IdentityUserId { get; set; }
    }
}