using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Services;

public class MarkService : IMarkService
{
    private readonly IMarkRepository _markRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;

    public MarkService(
        IMarkRepository markRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        ISubjectRepository subjectRepository,
        IMapper mapper)
    {
        _markRepository = markRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MarkDto>> GetAllMarksAsync()
    {
        var marks = await _markRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MarkDto>>(marks);
    }

    public async Task<MarkDto?> GetMarkByIdAsync(int id)
    {
        var mark = await _markRepository.GetByIdAsync(id);
        return mark != null ? _mapper.Map<MarkDto>(mark) : null;
    }

    public async Task<MarkDto?> GetMarkByStudentIdAsync(int studentId)
    {
        var mark = await _markRepository.GetByStudentIdAsync(studentId);
        return mark != null ? _mapper.Map<MarkDto>(mark) : null;
    }

    public async Task<IEnumerable<MarkDto>> GetMarksByCourseAsync(string courseName)
    {
        var marks = await _markRepository.GetByCourseAsync(courseName);
        return _mapper.Map<IEnumerable<MarkDto>>(marks);
    }

    public async Task<MarkDto> CreateMarkAsync(CreateMarkDto createMarkDto)
    {
        // Validate student exists
        var studentExists = await _studentRepository.ExistsByIdAsync(createMarkDto.StudentId);
        if (!studentExists)
        {
            throw new InvalidOperationException($"Student with ID {createMarkDto.StudentId} does not exist.");
        }

        // Check if marks already exist for this student
        var marksExist = await _markRepository.ExistsForStudentAsync(createMarkDto.StudentId);
        if (marksExist)
        {
            throw new InvalidOperationException($"Marks already exist for student with ID {createMarkDto.StudentId}. Use update instead.");
        }

        // Validate course exists
        if (!string.IsNullOrEmpty(createMarkDto.CourseName))
        {
            var courseExists = await _courseRepository.ExistsByNameAsync(createMarkDto.CourseName);
            if (!courseExists)
            {
                throw new InvalidOperationException($"Course '{createMarkDto.CourseName}' does not exist.");
            }
        }
        else
        {
            throw new InvalidOperationException("Course name is required.");
        }

        // Map DTO to entity
        var mark = _mapper.Map<Mark>(createMarkDto);
        
        // Calculate totals
        CalculateTotals(mark);
        
        // Save to database
        var createdMark = await _markRepository.AddAsync(mark);
        
        // Return the created mark as DTO
        return _mapper.Map<MarkDto>(createdMark);
    }

    public async Task UpdateMarkAsync(int id, CreateMarkDto updateMarkDto)
    {
        var mark = await _markRepository.GetByIdAsync(id);
        if (mark == null)
        {
            throw new KeyNotFoundException($"Mark with ID {id} not found.");
        }

        // Validate student exists if being changed
        if (updateMarkDto.StudentId != mark.StudentId)
        {
            var studentExists = await _studentRepository.ExistsByIdAsync(updateMarkDto.StudentId);
            if (!studentExists)
            {
                throw new InvalidOperationException($"Student with ID {updateMarkDto.StudentId} does not exist.");
            }
        }

        // Validate course exists if being changed
        if (!string.IsNullOrEmpty(updateMarkDto.CourseName) && 
            updateMarkDto.CourseName != mark.CourseName)
        {
            var courseExists = await _courseRepository.ExistsByNameAsync(updateMarkDto.CourseName);
            if (!courseExists)
            {
                throw new InvalidOperationException($"Course '{updateMarkDto.CourseName}' does not exist.");
            }
        }

        // Update properties
        _mapper.Map(updateMarkDto, mark);
        
        // Calculate totals
        CalculateTotals(mark);
        
        // Save changes
        await _markRepository.UpdateAsync(mark);
    }

    public async Task DeleteMarkAsync(int id)
    {
        var mark = await _markRepository.GetByIdAsync(id);
        if (mark == null)
        {
            throw new KeyNotFoundException($"Mark with ID {id} not found.");
        }

        await _markRepository.DeleteAsync(mark);
    }

    private void CalculateTotals(Mark mark)
    {
        // Calculate total theory marks
        int totalTheoryMarks = 0;
        int totalMaxTheoryMarks = 0;
        
        if (!string.IsNullOrEmpty(mark.Subject1TheoryMarks) && !string.IsNullOrEmpty(mark.Subject1MaxTheoryMarks))
        {
            if (int.TryParse(mark.Subject1TheoryMarks, out int theoryMarks) && 
                int.TryParse(mark.Subject1MaxTheoryMarks, out int maxTheoryMarks))
            {
                totalTheoryMarks += theoryMarks;
                totalMaxTheoryMarks += maxTheoryMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject2TheoryMarks) && !string.IsNullOrEmpty(mark.Subject2MaxTheoryMarks))
        {
            if (int.TryParse(mark.Subject2TheoryMarks, out int theoryMarks) && 
                int.TryParse(mark.Subject2MaxTheoryMarks, out int maxTheoryMarks))
            {
                totalTheoryMarks += theoryMarks;
                totalMaxTheoryMarks += maxTheoryMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject3TheoryMarks) && !string.IsNullOrEmpty(mark.Subject3MaxTheoryMarks))
        {
            if (int.TryParse(mark.Subject3TheoryMarks, out int theoryMarks) && 
                int.TryParse(mark.Subject3MaxTheoryMarks, out int maxTheoryMarks))
            {
                totalTheoryMarks += theoryMarks;
                totalMaxTheoryMarks += maxTheoryMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject4TheoryMarks) && !string.IsNullOrEmpty(mark.Subject4MaxTheoryMarks))
        {
            if (int.TryParse(mark.Subject4TheoryMarks, out int theoryMarks) && 
                int.TryParse(mark.Subject4MaxTheoryMarks, out int maxTheoryMarks))
            {
                totalTheoryMarks += theoryMarks;
                totalMaxTheoryMarks += maxTheoryMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject5TheoryMarks) && !string.IsNullOrEmpty(mark.Subject5MaxTheoryMarks))
        {
            if (int.TryParse(mark.Subject5TheoryMarks, out int theoryMarks) && 
                int.TryParse(mark.Subject5MaxTheoryMarks, out int maxTheoryMarks))
            {
                totalTheoryMarks += theoryMarks;
                totalMaxTheoryMarks += maxTheoryMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject6TheoryMarks) && !string.IsNullOrEmpty(mark.Subject6MaxTheoryMarks))
        {
            if (int.TryParse(mark.Subject6TheoryMarks, out int theoryMarks) && 
                int.TryParse(mark.Subject6MaxTheoryMarks, out int maxTheoryMarks))
            {
                totalTheoryMarks += theoryMarks;
                totalMaxTheoryMarks += maxTheoryMarks;
            }
        }
        
        // Calculate total practical marks
        int totalPracticalMarks = 0;
        int totalMaxPracticalMarks = 0;
        
        if (!string.IsNullOrEmpty(mark.Subject1PracticalMarks) && !string.IsNullOrEmpty(mark.Subject1MaxPracticalMarks))
        {
            if (int.TryParse(mark.Subject1PracticalMarks, out int practicalMarks) && 
                int.TryParse(mark.Subject1MaxPracticalMarks, out int maxPracticalMarks))
            {
                totalPracticalMarks += practicalMarks;
                totalMaxPracticalMarks += maxPracticalMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject2PracticalMarks) && !string.IsNullOrEmpty(mark.Subject2MaxPracticalMarks))
        {
            if (int.TryParse(mark.Subject2PracticalMarks, out int practicalMarks) && 
                int.TryParse(mark.Subject2MaxPracticalMarks, out int maxPracticalMarks))
            {
                totalPracticalMarks += practicalMarks;
                totalMaxPracticalMarks += maxPracticalMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject3PracticalMarks) && !string.IsNullOrEmpty(mark.Subject3MaxPracticalMarks))
        {
            if (int.TryParse(mark.Subject3PracticalMarks, out int practicalMarks) && 
                int.TryParse(mark.Subject3MaxPracticalMarks, out int maxPracticalMarks))
            {
                totalPracticalMarks += practicalMarks;
                totalMaxPracticalMarks += maxPracticalMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject4PracticalMarks) && !string.IsNullOrEmpty(mark.Subject4MaxPracticalMarks))
        {
            if (int.TryParse(mark.Subject4PracticalMarks, out int practicalMarks) && 
                int.TryParse(mark.Subject4MaxPracticalMarks, out int maxPracticalMarks))
            {
                totalPracticalMarks += practicalMarks;
                totalMaxPracticalMarks += maxPracticalMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject5PracticalMarks) && !string.IsNullOrEmpty(mark.Subject5MaxPracticalMarks))
        {
            if (int.TryParse(mark.Subject5PracticalMarks, out int practicalMarks) && 
                int.TryParse(mark.Subject5MaxPracticalMarks, out int maxPracticalMarks))
            {
                totalPracticalMarks += practicalMarks;
                totalMaxPracticalMarks += maxPracticalMarks;
            }
        }
        
        if (!string.IsNullOrEmpty(mark.Subject6PracticalMarks) && !string.IsNullOrEmpty(mark.Subject6MaxPracticalMarks))
        {
            if (int.TryParse(mark.Subject6PracticalMarks, out int practicalMarks) && 
                int.TryParse(mark.Subject6MaxPracticalMarks, out int maxPracticalMarks))
            {
                totalPracticalMarks += practicalMarks;
                totalMaxPracticalMarks += maxPracticalMarks;
            }
        }
        
        // Set total values
        mark.TotalTheoryMarks = totalTheoryMarks.ToString();
        mark.TotalMaxTheoryMarks = totalMaxTheoryMarks.ToString();
        mark.TotalPracticalMarks = totalPracticalMarks.ToString();
        mark.TotalMaxPracticalMarks = totalMaxPracticalMarks.ToString();
    }
}