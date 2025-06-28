using CMS.Application.DTOs;

namespace CMS.Application.Interfaces;

public interface IFacultyService
{
    Task<IEnumerable<FacultyDto>> GetAllFacultiesAsync();
    Task<FacultyDto?> GetFacultyByIdAsync(int id);
    Task<IEnumerable<FacultyDto>> GetFacultiesBySubjectAsync(string subjectName);
    Task<FacultyDto> CreateFacultyAsync(CreateFacultyDto createFacultyDto);
    Task UpdateFacultyAsync(int id, CreateFacultyDto updateFacultyDto);
    Task DeleteFacultyAsync(int id);
    Task UpdateFacultySubjectAsync(int id, string subjectName);
}