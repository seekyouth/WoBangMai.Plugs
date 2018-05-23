using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ExpressionParser.Plug;
using static AutoMapperTest.Program;

namespace AutoMapperTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////注册映射关系
            //Mapping.Register();

            ////数据初始化
            //var grade = new Grade { Id = "g001", Name = "一年级" };
            //var _class = new Class { Id = "c001", Name = "一班", GradeId = grade.Id, Grade = grade };
            //var student = new Student
            //{
            //    Id = "s001",
            //    Name = "Cigarette",
            //    Birthday = DateTime.Now,
            //    Gender = "Male",
            //    ClassId = _class.Id,
            //    Class = _class
            //};
            //_class.Students.Add(student);
            //grade.Classes.Add(_class);

            ////1.Student --> StudentDto
            //var studentDto = student.ToModel();
            ////2.StudentDto --> Student
            //student = studentDto.ToEntity();
            ////3.StudentDto --> Student(以一个已存在的Student作为基础)
            //var studentPart = new Student { Gender = "Female", Class = new Class { Name = "new class" } };
            //student = studentDto.ToEntity(studentPart);

            //Console.ReadKey();



            //List<User> myUsers = new List<User>();
            //var userSql = myUsers.AsQueryable().Where(u => u.Age > 2 & u.Name == "王大力");
            //Console.WriteLine(userSql);
            // SELECT* FROM(SELECT* FROM User) AS T WHERE(Age > 2)

            var name = "王大力";
            var Age = 2;
            Expression<Func<User, bool>> expressionUser = u => u.Age > Age & u.Name == name;
            var translator = new QueryTranslator();
            var userSql3 = translator.Translate(expressionUser);
            Console.WriteLine(userSql3);

            //List<User> myUsers2 = new List<User>();
            //var userSql2 = myUsers.AsQueryable().Where(u => u.Name == "Jesse");
            //Console.WriteLine(userSql2);
            //SELECT * FROM (SELECT * FROM USER) AS T WHERE (Name='Jesse')


            Console.ReadLine();


        }


        public partial class StudentDto
        {
            /// <summary>
            /// 学员编号 --> Student.Id
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// 姓名 --> Student.Name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 生日 --> Student.Birthday
            /// </summary>
            public DateTime? Birthday { get; set; }

            /// <summary>
            /// 班级编号 --> Student.ClassId
            /// </summary>
            public string ClassId { get; set; }

            /// <summary>
            /// 班级 --> Student.Class.Name
            /// </summary>
            public string ClassName { get; set; }

            /// <summary>
            /// 年级编号 Student.Class.GradeId
            /// </summary>
            public string GradeId { get; set; }

            /// <summary>
            /// 年级 Student.Class.Grade.Name
            /// </summary>
            public string GradeName { get; set; }
        }

        public partial class Student
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public DateTime? Birthday { get; set; }
            public string ClassId { get; set; }
            public virtual Class Class { get; set; }
        }

        public partial class Class
        {
            public Class()
            {
                this.Students = new List<Student>();
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public List<Student> Students { get; set; }
            public string GradeId { get; set; }
            public virtual Grade Grade { get; set; }
        }

        public partial class Grade
        {
            public Grade()
            {
                this.Classes = new List<Class>();
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public List<Class> Classes { get; set; }
        }



    }


    public static class Mapping
    {
        /// <summary>
        /// 注册映射关系 
        /// </summary>
        public static void Register()
        {

            var config = new MapperConfigurationExpression();
            config.CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.ClassName, mo => mo.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.GradeId, mo => mo.MapFrom(src => src.Class.GradeId))
                .ForMember(dest => dest.GradeName, mo => mo.MapFrom(src => src.Class.Grade.Name));
            //或者Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());

             config.CreateMap<StudentDto, Student>()
               .ForMember(dest => dest.Gender, mo => mo.Ignore())
               .ForMember(dest => dest.Class, mo => mo.Ignore());

            Mapper.Initialize(config);

            
        }

        /// <summary>
        /// 领域模型转化为Dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static StudentDto ToModel(this Student entity)
        {
            var config = new MapperConfiguration(m => m.CreateMap<Student, StudentDto>()
              .ForMember(dest => dest.ClassName, mo => mo.MapFrom(src => src.Class.Name))
              .ForMember(dest => dest.GradeId, mo => mo.MapFrom(src => src.Class.GradeId))
              .ForMember(dest => dest.GradeName, mo => mo.MapFrom(src => src.Class.Grade.Name)));
            //或者Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());

            var mapper = config.CreateMapper();
            return mapper.Map<Student, StudentDto>(entity);
        }

        /// <summary>
        /// Dto转化为领域模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Student ToEntity(this StudentDto model)
        {
            return Mapper.Map<StudentDto, Student>(model);
        }

        /// <summary>
        /// 重载 ToEntity, 在已有 Dto模型基础上使用领域模型转换成 Dto
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Student ToEntity(this StudentDto model, Student entity)
        {
            return Mapper.Map(model, entity);
        }

    }
}
