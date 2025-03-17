using AutoMapper;
using Company.G03.DAL.Models;
using Company.G03.PL.Dtos;

namespace Company.G03.PL.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();
        }
    }
}
