//+-------------------------+
//| Staff                   |
//+-------------------------+
//| - Id: int               |
//| - FirstName: string     |
//| - LastName: string      |
//| - Role: string          |
//| - Email: string         |
//| - PhoneNumber: string   |
//| - Department: string    |
//+-------------------------+
//| + GetFullName(): string |
//+-------------------------+

//Relatiosnhip
//+-------------------+1            *+-------------------+
//| Staff             |--------------| Appointment       |
//+-------------------+              +-------------------+
//| - Id              |              | - Id              |
//| - FirstName       |              | - PatientId       |
//| - LastName        |              | - StaffId         |
//| - Role            |              | - Date            |
//| - Email           |              | - Reason          |
//| - PhoneNumber     |              +-------------------+
//| - Department      |
//+-------------------+




using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// Representing the model of the <see cref="Staff"/>
    /// </summary>
    [Table("Staffs")]
    public class Staff
    {
        /// <summary>
        /// The unique identifier of the staff.
        /// </summary>
        [Key]
        public int Id { get; init; }

        /// <summary>
        /// The first name of the staff.
        /// </summary>
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the staff.
        /// </summary>
        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The role of the staff.
        /// </summary>
        [Required, MaxLength(50)]
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the staff.
        /// </summary>
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the staff.
        /// </summary>
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The department of the staff.
        /// </summary>
        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// The staff-related appointments.
        /// </summary>
        /// <remarks>
        /// Representing the relationship between <see cref="Appointment"/> and <see cref="Staff"/>
        /// </remarks>
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        /// <summary>
        /// Gets the full name of the staff.
        /// </summary>
        /// <returns>
        /// A string representing the full name of the staff.
        /// </returns>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
