using CMS.Application.DTOs;

namespace CMS.Application.Interfaces;

public interface IMarkService
{
    Task<IEnumerable<MarkDto>> GetAllMarksAsync();
    Task<MarkDto?> GetMarkByIdAsync(int id);
    Task<MarkDto?> GetMarkByStudentIdAsync(int studentId);
    Task<IEnumerable<MarkDto>> GetMarksByCourseAsync(string courseName);
    Task<MarkDto> CreateMarkAsync(CreateMarkDto createMarkDto);
    Task UpdateMarkAsync(int id, CreateMarkDto updateMarkDto);
    Task DeleteMarkAsync(int id);
}