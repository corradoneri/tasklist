using System.ComponentModel.DataAnnotations;

namespace Tasks.Core.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(64)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
