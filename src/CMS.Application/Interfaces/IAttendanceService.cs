using CMS.Application.DTOs;

namespace CMS.Application.Interfaces;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceDto>> GetAllAttendancesAsync();
    Task<AttendanceDto?> GetAttendanceByIdAsync(int id);
    Task<IEnumerable<AttendanceDto>> GetAttendancesByStudentIdAsync(int studentId);
    Task<IEnumerable<AttendanceDto>> GetAttendancesByDateAsync(string date);
    Task<IEnumerable<AttendanceDto>> GetAttendancesByCourseAndSubjectAsync(string courseName, string subjectName);
    Task<AttendanceDto> CreateAttendanceAsync(CreateAttendanceDto createAttendanceDto);
    Task DeleteAttendanceAsync(int id);
}