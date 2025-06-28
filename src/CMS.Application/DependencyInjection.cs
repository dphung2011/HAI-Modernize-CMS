using AutoMapper;
using CMS.Application.Interfaces;
using CMS.Application.Services;
using CMS.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register MediatR
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // Register FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register Cache Service
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();

        // Register application services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IFacultyService, FacultyService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<IMarkService, MarkService>();

        return services;
    }
}