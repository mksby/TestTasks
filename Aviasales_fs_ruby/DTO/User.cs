using System.ComponentModel.DataAnnotations;

namespace TestTasks.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}