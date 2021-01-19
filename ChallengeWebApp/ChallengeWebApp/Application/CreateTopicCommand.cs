using System.ComponentModel.DataAnnotations;

namespace ChallengeWebApp.Application
{
    public class CreateTopicCommand
    {
        [Required(ErrorMessage = "'Title' must not be empty.")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Maximum 150 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "'Description' must not be empty.")]
        public string Description { get; set; }
    }
}