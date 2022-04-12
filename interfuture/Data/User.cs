using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace interfuture.Data
{
    public class User
    {
        public User()
        {
            Tasks = new HashSet<Task>();
            Name = "";
        }

        public int Id { get; set; }

        private string _Email = "";
        [Email]
        [Required]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value.ToLower();
            }
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime LastModification { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
