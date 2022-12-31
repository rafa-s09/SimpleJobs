using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleJobs.UnitaryTests.Repository.Setup
{
    [Table("Course")]
    public class CourseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(200),Required]
        public string? Name { get; set; }
    }
}