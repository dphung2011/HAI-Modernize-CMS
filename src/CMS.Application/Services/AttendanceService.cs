using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public AttendanceService(
        IAttendanceRepository attendanceRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        ISubjectRepository subjectRepository,
        IMapper mapper)
    {
        _attendanceRepository = attendanceRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AttendanceDto>> GetAllAttendancesAsync()
    {
        var attendances = await _attendanceRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AttendanceDto>>(attendances);
    }

    public async Task<AttendanceDto?> GetAttendanceByIdAsync(int id)
    {
        var attendance = await _attendanceRepository.GetByIdAsync(id);
        return attendance != null ? _mapper.Map<AttendanceDto>(attendance) : null;
    }

    public async Task<IEnumerable<AttendanceDto>> GetAttendancesByStudentIdAsync(int studentId)
    {
        var attendances = await _attendanceRepository.GetByStudentIdAsync(studentId);
        return _mapper.Map<IEnumerable<AttendanceDto>>(attendances);
    }

    public async Task<IEnumerable<AttendanceDto>> GetAttendancesByDateAsync(string date)
    {
        var attendances = await _attendanceRepository.GetByDateAsync(date);
        return _mapper.Map<IEnumerable<AttendanceDto>>(attendances);
    }

    public async Task<IEnumerable<AttendanceDto>> GetAttendancesByCourseAndSubjectAsync(string courseName, string subjectName)
    {
        var attendances = await _attendanceRepository.GetByCourseAndSubjectAsync(courseName, subjectName);
        return _mapper.Map<IEnumerable<AttendanceDto>>(attendances);
    }

    public async Task<AttendanceDto> CreateAttendanceAsync(CreateAttendanceDto createAttendanceDto)
    {
        // Validate student exists
        if (createAttendanceDto.StudentId <= 0)
        {
            throw new InvalidOperationException("Student ID is required.");
        }

        // Validate course exists
        if (!string.IsNullOrEmpty(createAttendanceDto.CourseName))
        {
            var courseExists = await _courseRepository.ExistsByNameAsync(createAttendanceDto.CourseName);
            if (!courseExists)
            {
                throw new InvalidOperationException($"Course '{createAttendanceDto.CourseName}' does not exist.");
            }
        }
        else
        {
            throw new InvalidOperationException("Course name is required.");
        }

        // Validate subject exists
        if (!string.IsNullOrEmpty(createAttendanceDto.SubjectName))
        {
            var subject = await _subjectRepository.GetByNameAsync(createAttendanceDto.SubjectName);
            if (subject == null)
            {
                throw new InvalidOperationException($"Subject '{createAttendanceDto.SubjectName}' does not exist.");
            }
        }
        else
        {
            throw new InvalidOperationException("Subject name is required.");
        }

        // Set date if not provided
        if (string.IsNullOrEmpty(createAttendanceDto.Date))
        {
            createAttendanceDto.Date = DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Map DTO to entity
        var attendance = _mapper.Map<Attendance>(createAttendanceDto);
        
        // Save to database
        var createdAttendance = await _attendanceRepository.AddAsync(attendance);
        
        // Return the created attendance as DTO
        return _mapper.Map<AttendanceDto>(createdAttendance);
    }

    public async Task DeleteAttendanceAsync(int id)
    {
        var attendance = await _attendanceRepository.GetByIdAsync(id);
        if (attendance == null)
        {
            throw new KeyNotFoundException($"Attendance with ID {id} not found.");
        }

        await _attendanceRepository.DeleteAsync(attendance);
    }
}