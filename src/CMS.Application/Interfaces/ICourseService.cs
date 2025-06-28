using CMS.Application.DTOs;

namespace CMS.Application.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
    Task<CourseDto?> GetCourseByIdAsync(int id);
    Task<CourseDto?> GetCourseByNameAsync(string name);
    Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
    Task UpdateCourseAsync(int id, CreateCourseDto updateCourseDto);
    Task DeleteCourseAsync(int id);
}