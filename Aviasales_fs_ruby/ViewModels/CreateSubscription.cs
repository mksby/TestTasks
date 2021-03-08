using System.ComponentModel.DataAnnotations;

namespace TestTasks.ViewModels
{
    public class CreateSubscription
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int ProgramId { get; set; }
    }
}