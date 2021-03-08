using System.ComponentModel.DataAnnotations;

namespace TestTasks.ViewModels
{
    public class ProgramUserBan
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProgramId { get; set; }
        
        [Required]
        public bool Ban { get; set; }
    }
}