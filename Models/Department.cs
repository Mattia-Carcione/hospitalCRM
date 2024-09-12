//+------------------+1
//|  Department       |     
//+-------------------+  
//| - Id              |
//| - Name            |
//+-------------------+


//Relatiosnhip
//+-------------------+1
//| Staff             |
//+-------------------+ 
//| - Id              |
//| - FirstName       | 
//| - LastName        | 
//| - Role            | 
//| - Email           | 
//| - PhoneNumber     | 
//| - Department      |
//+-------------------+
//          |
//          |*
//          |
//          1
//+------------------+1
//|  Department       |     
//+-------------------+  
//| - Id              |
//| - Name            |
//+-------------------+

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// Representing the model of the <see cref="Department"/>.
    /// </summary>
    [Table("Departments")]
    public class Department
    {
        /// <summary>
        /// The unique identifier of the department.
        /// </summary>
        [Key]
        public int Id { get; init; }

        /// <summary>
        /// The name of the department.
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The Department-related Staffs.
        /// </summary>
        /// <remarks>
        /// Representing the relationtship between <see cref="Staff"/> and <see cref="Department"/>
        /// </remarks>
        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
    }
}
