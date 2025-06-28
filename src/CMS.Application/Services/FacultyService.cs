using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Services;

public class FacultyService : IFacultyService
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public FacultyService(
        IFacultyRepository facultyRepository, 
        ISubjectRepository subjectRepository,
        IMapper mapper)
    {
        _facultyRepository = facultyRepository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FacultyDto>> GetAllFacultiesAsync()
    {
        var faculties = await _facultyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FacultyDto>>(faculties);
    }

    public async Task<FacultyDto?> GetFacultyByIdAsync(int id)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id);
        return faculty != null ? _mapper.Map<FacultyDto>(faculty) : null;
    }

    public async Task<IEnumerable<FacultyDto>> GetFacultiesBySubjectAsync(string subjectName)
    {
        var faculties = await _facultyRepository.GetBySubjectAsync(subjectName);
        return _mapper.Map<IEnumerable<FacultyDto>>(faculties);
    }

    public async Task<FacultyDto> CreateFacultyAsync(CreateFacultyDto createFacultyDto)
    {
        // Validate subject exists if specified
        if (!string.IsNullOrEmpty(createFacultyDto.SubjectName))
        {
            var subject = await _subjectRepository.GetByNameAsync(createFacultyDto.SubjectName);
            if (subject == null)
            {
                throw new InvalidOperationException($"Subject '{createFacultyDto.SubjectName}' does not exist.");
            }
        }

        // Set join date if not provided
        if (string.IsNullOrEmpty(createFacultyDto.JoinDate))
        {
            createFacultyDto.JoinDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Map DTO to entity
        var faculty = _mapper.Map<Faculty>(createFacultyDto);
        
        // Save to database
        var createdFaculty = await _facultyRepository.AddAsync(faculty);
        
        // Return the created faculty as DTO
        return _mapper.Map<FacultyDto>(createdFaculty);
    }

    public async Task UpdateFacultyAsync(int id, CreateFacultyDto updateFacultyDto)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id);
        if (faculty == null)
        {
            throw new KeyNotFoundException($"Faculty with ID {id} not found.");
        }

        // Validate subject exists if being changed
        if (!string.IsNullOrEmpty(updateFacultyDto.SubjectName) && 
            updateFacultyDto.SubjectName != faculty.SubjectName)
        {
            var subject = await _subjectRepository.GetByNameAsync(updateFacultyDto.SubjectName);
            if (subject == null)
            {
                throw new InvalidOperationException($"Subject '{updateFacultyDto.SubjectName}' does not exist.");
            }
        }

        // Update properties
        _mapper.Map(updateFacultyDto, faculty);
        
        // Save changes
        await _facultyRepository.UpdateAsync(faculty);
    }

    public async Task DeleteFacultyAsync(int id)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id);
        if (faculty == null)
        {
            throw new KeyNotFoundException($"Faculty with ID {id} not found.");
        }

        await _facultyRepository.DeleteAsync(faculty);
    }

    public async Task UpdateFacultySubjectAsync(int id, string subjectName)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id);
        if (faculty == null)
        {
            throw new KeyNotFoundException($"Faculty with ID {id} not found.");
        }

        // Validate subject exists
        var subject = await _subjectRepository.GetByNameAsync(subjectName);
        if (subject == null)
        {
            throw new InvalidOperationException($"Subject '{subjectName}' does not exist.");
        }

        // Update subject
        faculty.SubjectName = subjectName;
        
        // Save changes
        await _facultyRepository.UpdateAsync(faculty);
    }
}