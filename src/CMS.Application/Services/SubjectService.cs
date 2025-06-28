using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public SubjectService(
        ISubjectRepository subjectRepository, 
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
    }

    public async Task<SubjectDto?> GetSubjectByIdAsync(int id)
    {
        var subject = await _subjectRepository.GetByIdAsync(id);
        return subject != null ? _mapper.Map<SubjectDto>(subject) : null;
    }

    public async Task<IEnumerable<SubjectDto>> GetSubjectsByCourseAsync(string courseName)
    {
        var subjects = await _subjectRepository.GetByCourseAsync(courseName);
        return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
    }

    public async Task<SubjectDto?> GetSubjectByNameAsync(string name)
    {
        var subject = await _subjectRepository.GetByNameAsync(name);
        return subject != null ? _mapper.Map<SubjectDto>(subject) : null;
    }

    public async Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto createSubjectDto)
    {
        // Validate course exists
        if (!string.IsNullOrEmpty(createSubjectDto.CourseName))
        {
            var courseExists = await _courseRepository.ExistsByNameAsync(createSubjectDto.CourseName);
            if (!courseExists)
            {
                throw new InvalidOperationException($"Course '{createSubjectDto.CourseName}' does not exist.");
            }
        }
        else
        {
            throw new InvalidOperationException("Course name is required.");
        }

        // Validate subject name is unique
        var existingSubject = await _subjectRepository.GetByNameAsync(createSubjectDto.SubjectName ?? string.Empty);
        if (existingSubject != null)
        {
            throw new InvalidOperationException($"Subject with name '{createSubjectDto.SubjectName}' already exists.");
        }

        // Map DTO to entity
        var subject = _mapper.Map<Subject>(createSubjectDto);
        
        // Save to database
        var createdSubject = await _subjectRepository.AddAsync(subject);
        
        // Return the created subject as DTO
        return _mapper.Map<SubjectDto>(createdSubject);
    }

    public async Task UpdateSubjectAsync(int id, CreateSubjectDto updateSubjectDto)
    {
        var subject = await _subjectRepository.GetByIdAsync(id);
        if (subject == null)
        {
            throw new KeyNotFoundException($"Subject with ID {id} not found.");
        }

        // Validate course exists if being changed
        if (!string.IsNullOrEmpty(updateSubjectDto.CourseName) && 
            updateSubjectDto.CourseName != subject.CourseName)
        {
            var courseExists = await _courseRepository.ExistsByNameAsync(updateSubjectDto.CourseName);
            if (!courseExists)
            {
                throw new InvalidOperationException($"Course '{updateSubjectDto.CourseName}' does not exist.");
            }
        }

        // Check if subject name is being changed and if it's unique
        if (!string.IsNullOrEmpty(updateSubjectDto.SubjectName) && 
            updateSubjectDto.SubjectName != subject.SubjectName)
        {
            var existingSubject = await _subjectRepository.GetByNameAsync(updateSubjectDto.SubjectName);
            if (existingSubject != null && existingSubject.SubjectId != id)
            {
                throw new InvalidOperationException($"Subject with name '{updateSubjectDto.SubjectName}' already exists.");
            }
        }

        // Update properties
        _mapper.Map(updateSubjectDto, subject);
        
        // Save changes
        await _subjectRepository.UpdateAsync(subject);
    }

    public async Task DeleteSubjectAsync(int id)
    {
        var subject = await _subjectRepository.GetByIdAsync(id);
        if (subject == null)
        {
            throw new KeyNotFoundException($"Subject with ID {id} not found.");
        }

        // In a real application, you might want to check if there are faculties assigned to this subject
        // and prevent deletion if there are, or implement a cascading delete

        await _subjectRepository.DeleteAsync(subject);
    }
}