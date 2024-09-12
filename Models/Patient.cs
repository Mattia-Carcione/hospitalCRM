//+-------------------------+
//| Patient                 |
//+-------------------------+
//| - Id: int               |
//| - FirstName: string     |
//| - LastName: string      |
//| - DateOfBirth: DateTime |
//| - Address: string       |
//| - PhoneNumber: string   |
//| - Email: string         |
//+-------------------------+
//| + GetFullName(): string |
//+-------------------------+

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
//| -Email            |
//+-------------------+
//         |
//         | 1
//         |
//         *
//+---------------------+
//| MedicalRecord       |
//+---------------------+
//| - Id                |
//| - PatientId         |
//| - RecordDate        |
//| - Diagnosis         |
//| - Treatment         |
//| - Notes             |
//+---------------------+

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// Representing the model of <see cref="Patient"/>.
    /// </summary>
    [Table("Patients")]
    public class Patient
    {
        /// <summary>
        /// The unique identifier of the patient.
        /// </summary>
        [Key]
        public int Id { get; init; }

        /// <summary>
        /// The first name of the patient.
        /// </summary>
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the patient.
        /// </summary>
        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The birthday of the patient.
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Where the patient lives.
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the patient.
        /// </summary>
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the patient.
        /// </summary>
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The patient list of the appointments.
        /// </summary>
        /// <remarks>
        /// Representing the relationship between <see cref="Patient"/> and <see cref="Appointment"/>.
        /// </remarks>
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        /// <summary>
        /// The patient list of the medical record.
        /// </summary>
        /// <remarks>
        /// Representing the relationship between <see cref="Patient"/> and <see cref="MedicalRecord"/>.
        /// </remarks>
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

        /// <summary>
        /// Gets the full name of the patient.
        /// </summary>
        /// <returns>
        /// A string representing the full name of the patient.
        /// </returns>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
