using Microsoft.EntityFrameworkCore;
using WFM.Database.Models;

namespace WFM.Database.DbContext;

public partial class WfmContext : Microsoft.EntityFrameworkCore.DbContext
{
    public WfmContext()
    {
    }

    public WfmContext(DbContextOptions<WfmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesBranchesHistory> EmployeesBranchesHistories { get; set; }

    public virtual DbSet<EmployeesJobHistory> EmployeesJobHistories { get; set; }

    public virtual DbSet<ExperienceLevel> ExperienceLevels { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vacation> Vacations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("branches_pkey");

            entity.ToTable("branches", "business");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .HasColumnName("branch_name");
            entity.Property(e => e.CountryId).HasColumnName("country_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Branches)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("branches_country_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("countries_pkey");

            entity.ToTable("countries", "general");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("departments_pkey");

            entity.ToTable("departments", "business");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");

            entity.ToTable("employees", "business");

            entity.HasIndex(e => e.PayrollId, "employees_payroll_id_key").IsUnique();

            entity.HasIndex(e => e.UserId, "employees_user_id_key").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfEmployment).HasColumnName("date_of_employment");
            entity.Property(e => e.DateOfLeaving).HasColumnName("date_of_leaving");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.EmployeeAddress)
                .HasMaxLength(255)
                .HasColumnName("employee_address");
            entity.Property(e => e.ExperienceLevelId).HasColumnName("experience_level_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.NationalityId).HasColumnName("nationality_id");
            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");
            entity.Property(e => e.RemainingVacationDays)
                .HasDefaultValue(25)
                .HasColumnName("remaining_vacation_days");
            entity.Property(e => e.UsedVacationDays)
                .HasDefaultValue(0)
                .HasColumnName("used_vacation_days");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_branch_id_fkey");

            entity.HasOne(d => d.ExperienceLevel).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ExperienceLevelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_experience_level_id_fkey");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_job_id_fkey");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Employees)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_nationality_id_fkey");

            entity.HasOne(d => d.Payroll).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.PayrollId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_payroll_id_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_user_id_fkey");
        });

        modelBuilder.Entity<EmployeesBranchesHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeBranchId).HasName("employees_branches_history_pkey");

            entity.ToTable("employees_branches_history", "history");

            entity.Property(e => e.EmployeeBranchId).HasColumnName("employee_branch_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.DateEnded).HasColumnName("date_ended");
            entity.Property(e => e.DateStarted).HasColumnName("date_started");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.EmployeesBranchesHistories)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("employees_branches_history_branch_id_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesBranchesHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("employees_branches_history_employee_id_fkey");
        });

        modelBuilder.Entity<EmployeesJobHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeJobId).HasName("employees_job_history_pkey");

            entity.ToTable("employees_job_history", "history");

            entity.Property(e => e.EmployeeJobId).HasColumnName("employee_job_id");
            entity.Property(e => e.DateEnded).HasColumnName("date_ended");
            entity.Property(e => e.DateStarted).HasColumnName("date_started");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesJobHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("employees_job_history_employee_id_fkey");

            entity.HasOne(d => d.Job).WithMany(p => p.EmployeesJobHistories)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("employees_job_history_job_id_fkey");
        });

        modelBuilder.Entity<ExperienceLevel>(entity =>
        {
            entity.HasKey(e => e.ExperienceLevelId).HasName("experience_levels_pkey");

            entity.ToTable("experience_levels", "general");

            entity.Property(e => e.ExperienceLevelId).HasColumnName("experience_level_id");
            entity.Property(e => e.ExperienceLevelTitle)
                .HasMaxLength(20)
                .HasColumnName("experience_level_title");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("jobs_pkey");

            entity.ToTable("jobs", "business");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .HasColumnName("job_title");

            entity.HasOne(d => d.Department).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("jobs_department_id_fkey");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("payrolls_pkey");

            entity.ToTable("payrolls", "business");

            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");
            entity.Property(e => e.EffectiveDate).HasColumnName("effective_date");
            entity.Property(e => e.GrossSalary).HasColumnName("gross_salary");
            entity.Property(e => e.HourlyRate).HasColumnName("hourly_rate");
            entity.Property(e => e.LastChanged).HasColumnName("last_changed");
            entity.Property(e => e.NetSalary).HasColumnName("net_salary");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles", "security");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleTitle)
                .HasMaxLength(21)
                .HasColumnName("role_title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users", "security");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<Vacation>(entity =>
        {
            entity.HasKey(e => e.VacationId).HasName("vacations_pkey");

            entity.ToTable("vacations", "history");

            entity.Property(e => e.VacationId).HasColumnName("vacation_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("reason");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.Vacations)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("vacations_employee_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
