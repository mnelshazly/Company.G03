using AutoMapper;
using Company.G03.DAL.Models;
using Company.G03.PL.Dtos;

namespace Company.G03.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            //CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
