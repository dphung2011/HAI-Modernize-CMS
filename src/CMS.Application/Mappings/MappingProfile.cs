using AutoMapper;
using CMS.Application.DTOs;
using CMS.Domain.Entities;

namespace CMS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Student mappings
        CreateMap<Student, StudentDto>();
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Faculty mappings
        CreateMap<Faculty, FacultyDto>();
        CreateMap<CreateFacultyDto, Faculty>();

        // Course mappings
        CreateMap<Course, CourseDto>();
        CreateMap<CreateCourseDto, Course>();

        // Subject mappings
        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateSubjectDto, Subject>();

        // Attendance mappings
        CreateMap<Attendance, AttendanceDto>();
        CreateMap<CreateAttendanceDto, Attendance>();

        // Mark mappings
        CreateMap<Mark, MarkDto>();
        CreateMap<CreateMarkDto, Mark>();
    }
}