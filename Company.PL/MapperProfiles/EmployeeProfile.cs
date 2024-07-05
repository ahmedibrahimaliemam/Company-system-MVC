using AutoMapper;
using Company.DL.Models;
using Company.PL.Models;

namespace Company.PL.MapperProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
