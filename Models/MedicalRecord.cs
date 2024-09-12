//+-----------------------+
//| MedicalRecord         |
//+-----------------------+
//| - Id: int             |
//| - PatientId: int      |
//| - RecordDate: DateTime|
//| - Diagnosis: string   |
//| - Treatment: string   |
//| - Notes: string       |
//+-----------------------+
//| + AddNote(): void     |
//+-----------------------+


//Relationship
//+-------------------+ 
//| Patient           |
//+-------------------+
//| -Id               |        
//| -FirstName        |        
//| -LastName         |      
//| -DateOfBirth      |
//| -Address          |        
//| -PhoneNumber      |         
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
    /// Representing the model of <see cref="MedicalRecord"/>.
    /// </summary>
    [Table("MedicalRecords")]
    public class MedicalRecord
    {
        /// <summary>
        /// The unique identifier of the medical record.
        /// </summary>
        [Key]
        public int Id { get; init; }

        /// <summary>
        /// The identifier of the patient.
        /// </summary>
        /// <remarks>
        /// Representing the relationtship between <see cref="Patient"/> and <see cref="MedicalRecord"/>
        /// </remarks>
        public int PatientId { get; set; }

        /// <summary>
        /// The medical record-related patient.
        /// </summary>
        /// <remarks>
        /// Representing the relationtship between <see cref="Patient"/> and <see cref="MedicalRecord"/>
        /// </remarks>
        public Patient? Patient { get; set; }

        /// <summary>
        /// The date of the medical record.
        /// </summary>
        [Required]
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// The diagnosis of the medical record.
        /// </summary>
        [MaxLength(400)]
        public string Diagnosis { get; set; } = string.Empty;

        /// <summary>
        /// The treatment given to the patient.
        /// </summary>
        [MaxLength(400)]
        public string Treatment { get; set; } = string.Empty;

        /// <summary>
        /// The notes of the medical record.
        /// </summary>
        public ICollection<string> Notes { get; set; } = new List<string>();

        /// <summary>
        /// Adds a note to the medical record.
        /// </summary>
        /// <param name="note">The note to be added</param>
        public void AddNote(string note)
        {
            Notes.Add(note);
        }
    }
}
