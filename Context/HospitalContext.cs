using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    /// <summary>
    /// An instance of <see cref="HospitalContext"/> representing the context.
    /// </summary>
    /// <remarks>
    /// Extends <see cref="DbContext"/>.
    /// </remarks>
    public class HospitalContext : DbContext
    {
        /// <summary>
        /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Appointment"/> entity.
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="MedicalRecord"/> entity.
        /// </summary>
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        /// <summary>
        /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Patient"/> entity.
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Staff"/> entity.
        /// </summary>
        public DbSet<Staff> Staffs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalContext"/> using the specified <paramref name="options"/> of <see cref="DbContextOptions{TContext}"/>.
        /// </summary>
        /// <param name="options">The options to be used by a db context.</param>
        public HospitalContext(DbContextOptions<HospitalContext> options) 
            : base(options) { }

        /// <summary>
        /// Configures the model and relationships between entities, and seeds initial data into the database using <see cref="ModelBuilder"/>.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Patient configuration
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            // Appointment configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Reason).HasMaxLength(400);
                entity.Property(e => e.PatientId).IsRequired();
                entity.HasOne(e => e.Patient)
                      .WithMany()
                      .HasForeignKey(e => e.PatientId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.StaffId).IsRequired();
                entity.HasOne(e => e.Staff)
                      .WithMany()
                      .HasForeignKey(e => e.StaffId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Staff configuration
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staffs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.DepartmentId).IsRequired();
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Staffs)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasMany(e => e.Staffs)
                      .WithOne(s => s.Department)
                      .HasForeignKey(s => s.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // MedicalRecord configuration
            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.ToTable("MedicalRecords");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RecordDate).IsRequired();
                entity.Property(e => e.Diagnosis).HasMaxLength(400);
                entity.Property(e => e.Treatment).HasMaxLength(400);
                entity.Property(e => e.PatientId).IsRequired();
                entity.HasOne(e => e.Patient)
                      .WithMany()
                      .HasForeignKey(e => e.PatientId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.Notes).HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries)
                );
            });

            // Seeding Patient
            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, FirstName = "Mario", LastName = "Rossi", Birthdate = new DateTime(1980, 5, 15), Address = "Via Roma 10, Milano", PhoneNumber = "+39 02 1234567", Email = "mario.rossi@example.com" },
                new Patient { Id = 2, FirstName = "Giulia", LastName = "Bianchi", Birthdate = new DateTime(1992, 11, 22), Address = "Corso Venezia 20, Milano", PhoneNumber = "+39 02 2345678", Email = "giulia.bianchi@example.com" },
                new Patient { Id = 3, FirstName = "Luca", LastName = "Verdi", Birthdate = new DateTime(1975, 7, 30), Address = "Via Torino 5, Torino", PhoneNumber = "+39 011 3456789", Email = "luca.verdi@example.com" },
                new Patient { Id = 4, FirstName = "Francesca", LastName = "Neri", Birthdate = new DateTime(1988, 3, 12), Address = "Piazza Duomo 1, Firenze", PhoneNumber = "+39 055 4567890", Email = "francesca.neri@example.com" },
                new Patient { Id = 5, FirstName = "Alessandro", LastName = "Russo", Birthdate = new DateTime(1990, 6, 8), Address = "Via San Giovanni 8, Napoli", PhoneNumber = "+39 081 5678901", Email = "alessandro.russo@example.com" }
            );

            // Seeding Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { Id = 101, FirstName = "Dr. Carlo", LastName = "Marini", Role = "Medico", Email = "carlo.marini@ospedale.com", PhoneNumber = "+39 02 3456789", DepartmentId = 1 },
                new Staff { Id = 102, FirstName = "Dr. Lucia", LastName = "Galli", Role = "Medico", Email = "lucia.galli@ospedale.com", PhoneNumber = "+39 02 4567890", DepartmentId = 2 },
                new Staff { Id = 103, FirstName = "Dr. Andrea", LastName = "Ferri", Role = "Medico", Email = "andrea.ferri@ospedale.com", PhoneNumber = "+39 011 2345678", DepartmentId = 3 },
                new Staff { Id = 104, FirstName = "Dr. Marta", LastName = "Morelli", Role = "Chirurgo", Email = "marta.morelli@ospedale.com", PhoneNumber = "+39 055 6789012", DepartmentId = 4 },
                new Staff { Id = 105, FirstName = "Dr. Giovanni", LastName = "Romano", Role = "Medico", Email = "giovanni.romano@ospedale.com", PhoneNumber = "+39 081 3456789", DepartmentId = 5 }
            );

            // Seeding Department
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Cardiologia" },
                new Department { Id = 2, Name = "Oncologia" },
                new Department { Id = 3, Name = "Ortopedia" },
                new Department { Id = 4, Name = "Chirurgia" },
                new Department { Id = 5, Name = "Neurologia" }
            );

            // Seeding Appointment
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, PatientId = 1, StaffId = 101, Date = new DateTime(2024, 10, 1, 9, 0, 0), Reason = "Controllo annuale" },
                new Appointment { Id = 2, PatientId = 2, StaffId = 102, Date = new DateTime(2024, 10, 2, 14, 30, 0), Reason = "Visita specialistica" },
                new Appointment { Id = 3, PatientId = 3, StaffId = 103, Date = new DateTime(2024, 10, 3, 11, 0, 0), Reason = "Controllo post-operatorio" },
                new Appointment { Id = 4, PatientId = 4, StaffId = 104, Date = new DateTime(2024, 10, 4, 16, 0, 0), Reason = "Consultazione iniziale" },
                new Appointment { Id = 5, PatientId = 5, StaffId = 105, Date = new DateTime(2024, 10, 5, 10, 0, 0), Reason = "Follow-up" }
            );

            // Seeding Medical Records
            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord { Id = 1, PatientId = 1, RecordDate = new DateTime(2024, 10, 1), Diagnosis = "Ipertensione", Treatment = "Farmaci per pressione alta", Notes = new List<string> { "Inizio del trattamento con nuovi farmaci." } },
                new MedicalRecord { Id = 2, PatientId = 2, RecordDate = new DateTime(2024, 10, 2), Diagnosis = "Diabete di tipo 2", Treatment = "Dieta e insulina", Notes = new List<string> { "Monitoraggio regolare dei livelli di zucchero." } },
                new MedicalRecord { Id = 3, PatientId = 3, RecordDate = new DateTime(2024, 10, 3), Diagnosis = "Frattura del femore", Treatment = "Intervento chirurgico e riabilitazione", Notes = new List<string> { "Recupero post-operatorio in corso." } },
                new MedicalRecord { Id = 4, PatientId = 4, RecordDate = new DateTime(2024, 10, 4), Diagnosis = "Asma", Treatment = "Inalatori e farmaci antinfiammatori", Notes = new List<string> { "Controllo della funzionalità polmonare necessario." } },
                new MedicalRecord { Id = 5, PatientId = 5, RecordDate = new DateTime(2024, 10, 5), Diagnosis = "Gastrite", Treatment = "Farmaci antiacidi e modifiche alla dieta", Notes = new List<string> { "Monitoraggio dei sintomi e delle reazioni ai farmaci." } }
            );
        }
    }
}
