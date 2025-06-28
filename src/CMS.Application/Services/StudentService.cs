using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public StudentService(
        IStudentRepository studentRepository, 
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        return student != null ? _mapper.Map<StudentDto>(student) : null;
    }

    public async Task<IEnumerable<StudentDto>> GetStudentsByCourseAsync(string courseName)
    {
        var students = await _studentRepository.GetByCourseName(courseName);
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto)
    {
        // Validate course exists if specified
        if (!string.IsNullOrEmpty(createStudentDto.CourseName))
        {
            var courseExists = await _courseRepository.ExistsByNameAsync(createStudentDto.CourseName);
            if (!courseExists)
            {
                throw new InvalidOperationException($"Course '{createStudentDto.CourseName}' does not exist.");
            }
        }

        // Set admission date if not provided
        if (string.IsNullOrEmpty(createStudentDto.AdmissionDate))
        {
            createStudentDto.AdmissionDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Map DTO to entity
        var student = _mapper.Map<Student>(createStudentDto);
        
        // Save to database
        var createdStudent = await _studentRepository.AddAsync(student);
        
        // Return the created student as DTO
        return _mapper.Map<StudentDto>(createdStudent);
    }

    public async Task UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            throw new KeyNotFoundException($"Student with ID {id} not found.");
        }

        // Validate course exists if being changed
        if (!string.IsNullOrEmpty(updateStudentDto.CourseName) && 
            updateStudentDto.CourseName != student.CourseName)
        {
            var courseExists = await _courseRepository.ExistsByNameAsync(updateStudentDto.CourseName);
            if (!courseExists)
            {
                throw new InvalidOperationException($"Course '{updateStudentDto.CourseName}' does not exist.");
            }
        }

        // Update properties
        _mapper.Map(updateStudentDto, student);
        
        // Save changes
        await _studentRepository.UpdateAsync(student);
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            throw new KeyNotFoundException($"Student with ID {id} not found.");
        }

        await _studentRepository.DeleteAsync(student);
    }

    public async Task UpdateStudentCourseAsync(int id, string courseName)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            throw new KeyNotFoundException($"Student with ID {id} not found.");
        }

        // Validate course exists
        var courseExists = await _courseRepository.ExistsByNameAsync(courseName);
        if (!courseExists)
        {
            throw new InvalidOperationException($"Course '{courseName}' does not exist.");
        }

        // Update course
        student.CourseName = courseName;
        
        // Save changes
        await _studentRepository.UpdateAsync(student);
    }
}