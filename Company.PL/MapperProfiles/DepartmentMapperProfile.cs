using AutoMapper;
using Company.DL.Models;
using Company.PL.Models;

namespace Company.PL.MapperProfiles
{
    public class DepartmentMapperProfile:Profile
    {
        public DepartmentMapperProfile()
        {
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
