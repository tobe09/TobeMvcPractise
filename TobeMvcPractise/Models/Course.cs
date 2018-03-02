using System.ComponentModel.DataAnnotations.Schema;

namespace TobeMvcPractise.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public int? Migrate { get; set; }

        [ForeignKey("Employee")]        //needed for navigation property
        public int Employee_Id { get; set; }

        public Employee Employee { get; set; }
    }
}