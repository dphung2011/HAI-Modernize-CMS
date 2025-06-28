using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Application.Services;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private const string CacheKeyPrefix = "Course_";
    private const string AllCoursesKey = "AllCourses";

    public CourseService(
        ICourseRepository courseRepository, 
        IMapper mapper,
        ICacheService cacheService)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        // Try to get from cache first
        if (_cacheService.TryGetValue<IEnumerable<CourseDto>>(AllCoursesKey, out var cachedCourses))
        {
            return cachedCourses;
        }

        // If not in cache, get from database
        var courses = await _courseRepository.GetAllAsync();
        var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);
        
        // Store in cache for future requests
        _cacheService.Set(AllCoursesKey, courseDtos, TimeSpan.FromMinutes(30));
        
        return courseDtos;
    }

    public async Task<CourseDto?> GetCourseByIdAsync(int id)
    {
        string cacheKey = $"{CacheKeyPrefix}{id}";
        
        // Try to get from cache first
        if (_cacheService.TryGetValue<CourseDto>(cacheKey, out var cachedCourse))
        {
            return cachedCourse;
        }

        // If not in cache, get from database
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
        {
            return null;
        }
        
        var courseDto = _mapper.Map<CourseDto>(course);
        
        // Store in cache for future requests
        _cacheService.Set(cacheKey, courseDto, TimeSpan.FromMinutes(30));
        
        return courseDto;
    }

    public async Task<CourseDto?> GetCourseByNameAsync(string name)
    {
        string cacheKey = $"{CacheKeyPrefix}name_{name}";
        
        // Try to get from cache first
        if (_cacheService.TryGetValue<CourseDto>(cacheKey, out var cachedCourse))
        {
            return cachedCourse;
        }

        // If not in cache, get from database
        var course = await _courseRepository.GetByNameAsync(name);
        if (course == null)
        {
            return null;
        }
        
        var courseDto = _mapper.Map<CourseDto>(course);
        
        // Store in cache for future requests
        _cacheService.Set(cacheKey, courseDto, TimeSpan.FromMinutes(30));
        
        return courseDto;
    }

    public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        // Check if course name is unique
        var existingCourse = await _courseRepository.GetByNameAsync(createCourseDto.CourseName ?? string.Empty);
        if (existingCourse != null)
        {
            throw new InvalidOperationException($"Course with name '{createCourseDto.CourseName}' already exists.");
        }

        // Map DTO to entity
        var course = _mapper.Map<Course>(createCourseDto);
        
        // Save to database
        var createdCourse = await _courseRepository.AddAsync(course);
        
        // Invalidate cache
        _cacheService.Remove(AllCoursesKey);
        
        // Return the created course as DTO
        return _mapper.Map<CourseDto>(createdCourse);
    }

    public async Task UpdateCourseAsync(int id, CreateCourseDto updateCourseDto)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
        {
            throw new KeyNotFoundException($"Course with ID {id} not found.");
        }

        // Check if course name is being changed and if it's unique
        if (updateCourseDto.CourseName != course.CourseName && !string.IsNullOrEmpty(updateCourseDto.CourseName))
        {
            var existingCourse = await _courseRepository.GetByNameAsync(updateCourseDto.CourseName);
            if (existingCourse != null && existingCourse.CourseId != id)
            {
                throw new InvalidOperationException($"Course with name '{updateCourseDto.CourseName}' already exists.");
            }
        }

        // Update properties
        _mapper.Map(updateCourseDto, course);
        
        // Save changes
        await _courseRepository.UpdateAsync(course);
        
        // Invalidate cache
        _cacheService.Remove(AllCoursesKey);
        _cacheService.Remove($"{CacheKeyPrefix}{id}");
        if (!string.IsNullOrEmpty(course.CourseName))
        {
            _cacheService.Remove($"{CacheKeyPrefix}name_{course.CourseName}");
        }
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
        {
            throw new KeyNotFoundException($"Course with ID {id} not found.");
        }

        // In a real application, you might want to check if there are students enrolled in this course
        // and prevent deletion if there are, or implement a cascading delete

        await _courseRepository.DeleteAsync(course);
        
        // Invalidate cache
        _cacheService.Remove(AllCoursesKey);
        _cacheService.Remove($"{CacheKeyPrefix}{id}");
        if (!string.IsNullOrEmpty(course.CourseName))
        {
            _cacheService.Remove($"{CacheKeyPrefix}name_{course.CourseName}");
        }
    }
}