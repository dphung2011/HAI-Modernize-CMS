using CMS.Application.DTOs;

namespace CMS.Application.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    Task<StudentDto?> GetStudentByIdAsync(int id);
    Task<IEnumerable<StudentDto>> GetStudentsByCourseAsync(string courseName);
    Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
    Task UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto);
    Task DeleteStudentAsync(int id);
    Task UpdateStudentCourseAsync(int id, string courseName);
}