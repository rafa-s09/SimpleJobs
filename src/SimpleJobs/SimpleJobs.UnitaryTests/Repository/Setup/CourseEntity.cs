namespace SimpleJobs.UnitaryTests.Repository.Setup;

[Table("Course")]
public class CourseEntity
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(200), Required]
    public string Name { get; set; } = string.Empty;
}