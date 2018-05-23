using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoBangMaiSDK
{
    public static class Mapping
    {
        /// <summary>
        /// 注册映射关系 
        /// </summary>
        public static void Register()
        {
            var config = new MapperConfigurationExpression();
            //config.CreateMap<Student, StudentDto>()
            //    .ForMember(dest => dest.ClassName, mo => mo.MapFrom(src => src.Class.Name))
            //    .ForMember(dest => dest.GradeId, mo => mo.MapFrom(src => src.Class.GradeId))
            //    .ForMember(dest => dest.GradeName, mo => mo.MapFrom(src => src.Class.Grade.Name));
            ////或者Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());

            //config.CreateMap<StudentDto, Student>()
            //  .ForMember(dest => dest.Gender, mo => mo.Ignore())
            //  .ForMember(dest => dest.Class, mo => mo.Ignore());
            Mapper.Initialize(config);
        }
    }
}
