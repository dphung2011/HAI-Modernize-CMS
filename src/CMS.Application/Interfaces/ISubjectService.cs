using CMS.Application.DTOs;

namespace CMS.Application.Interfaces;

public interface ISubjectService
{
    Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync();
    Task<SubjectDto?> GetSubjectByIdAsync(int id);
    Task<IEnumerable<SubjectDto>> GetSubjectsByCourseAsync(string courseName);
    Task<SubjectDto?> GetSubjectByNameAsync(string name);
    Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto createSubjectDto);
    Task UpdateSubjectAsync(int id, CreateSubjectDto updateSubjectDto);
    Task DeleteSubjectAsync(int id);
}