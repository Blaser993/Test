using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{

    [Table("post")]
    [Index(nameof(Id), IsUnique = true)]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
