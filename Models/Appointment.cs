//+-----------------------+
//| Appointment           |
//+-----------------------+
//| - Id: int             |
//| - PatientId: int      |
//| - StaffId: int        |
//| - Date: DateTime      |
//| - Reason: string      |
//+-----------------------+
//| + Schedule(): void    |
//| + Cancel(): void      |
//+-----------------------+
//
//Relationship
//+-------------------+         +---------------------+
//| Patient           | 1      *| Appointment         |
//+-------------------+---------+---------------------+
//| -Id               |         | - Id                |
//| -FirstName        |         | -PatientId          |
//| -LastName         |         | -StaffId            |
//| -DateOfBirth      |         | -Date               |
//| -Address          |         | -Reason             |
//| -PhoneNumber      |         +---------------------+
//| -Email            |                   |
//+-------------------+                   |*
//                                        |
//                                        1
//                              +-------------------+ 
//                              | Staff             | 
//                              +-------------------+  
//                              | - Id              |  
//                              | - FirstName       |   
//                              | - LastName        |    
//                              | - Role            |   
//                              | - Email           |   
//                              | - PhoneNumber     |  
//                              | - Department      |
//                              +-------------------+

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// Representing the model of the <see cref="Appointment"/>.
    /// </summary>
    [Table("Appointments")]
    public class Appointment
    {
        /// <summary>
        /// The unique identifier of the appointment.
        /// </summary>
        [Key]
        public int Id { get; init; }

        /// <summary>
        /// The identifier of the patient.
        /// </summary>
        /// <remarks>
        /// Representing the relationtship between <see cref="Patient"/> and <see cref="Appointment"/>
        /// </remarks>
        [Required]
        public int PatientId { get; set; }

        /// <summary>
        /// The medical record-related patient.
        /// </summary>
        /// <remarks>
        /// Representing the relationtship between <see cref="Patient"/> and <see cref="Appointment"/>
        /// </remarks>
        public Patient? Patient { get; set; }

        /// <summary>
        /// The identifier of the Staff.
        /// </summary>
        /// <remarks>
        /// Representing the relationtship between <see cref="Staff"/> and <see cref="Appointment"/>
        /// </remarks>
        [Required]
        public int StaffId { get; set; }

        /// <summary>
        /// The appointment-related staff.
        /// </summary>
        /// <remarks>
        /// Representing the relationtship between <see cref="Staff"/> and <see cref="Appointment"/>
        /// </remarks>
        public Staff? Staff { get; set; }

        /// <summary>
        /// The date of the appointment.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// The reason of the appointment.
        /// </summary>
        [MaxLength(400)]
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// The cancellation state of the appointment
        /// </summary>
        public bool IsCanceled { get; private set; } = false;

        /// <summary>
        /// Sets to false the cancellation state of the appointment.
        /// </summary>
        public void Schedule(Patient patient)
        {
            IsCanceled = false;
        }

        /// <summary>
        /// Sets to true the cancellation state of the appointment.
        /// </summary>
        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
