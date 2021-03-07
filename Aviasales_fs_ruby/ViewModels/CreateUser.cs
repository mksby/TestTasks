using System.ComponentModel.DataAnnotations;

namespace TestTasks.ViewModels
{
    public class CreateUser
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}