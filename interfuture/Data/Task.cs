using System.ComponentModel.DataAnnotations.Schema;

namespace interfuture.Data
{
    public class Task
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
