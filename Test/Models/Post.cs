using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    [Table("post")]
    [Index(nameof(Id), IsUnique = true)]
    public class Post
    {
        [Key]
        public int UserId { get; set; }
        public int Id { get; set; }

        [MaxLength(1024, ErrorMessage = "La lunghezza massima per il nome del titolo è di 1024 caratteri spazi compresi")]
        public string? Title { get; set; }

        [MaxLength(2048, ErrorMessage = "La lunghezza massima per il nome del titolo è di 2048 caratteri spazi compresi")]
        public string? Body { get; set; }
    }
}
