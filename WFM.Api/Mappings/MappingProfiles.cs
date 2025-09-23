using AutoMapper;
using WFM.Database.Models;
using WFM.Database.Repositories;
using WFM.UxModels.Models;

namespace WFM.Api.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Country, CountryUxModel>().ReverseMap();
        CreateMap<Branch, BranchUxModel>().ReverseMap();
        CreateMap<Department, DepartmentUxModel>().ReverseMap();
        CreateMap<Employee, EmployeeUxModel>().ReverseMap();
        CreateMap<ExperienceLevel, ExperienceLevelUxModel>().ReverseMap();
        CreateMap<EmployeesBranchesHistory, EmployeesBranchesHistoryUxModel>().ReverseMap();
        CreateMap<EmployeesJobHistoryRepository, EmployeesJobHistoryUxModel>().ReverseMap();
        CreateMap<User, UserUxModel>().ReverseMap();
        CreateMap<Job, JobUxModel>().ReverseMap();
        CreateMap<Payroll, PayrollUxModel>().ReverseMap();
        CreateMap<Role, RoleUxModel>().ReverseMap();
        CreateMap<Vacation, VacationUxModel>().ReverseMap();
    }
}