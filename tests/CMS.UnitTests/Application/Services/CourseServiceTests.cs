using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Services;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using Moq;
using Xunit;

namespace CMS.UnitTests.Application.Services;

public class CourseServiceTests
{
    private readonly Mock<ICourseRepository> _mockCourseRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ICacheService> _mockCacheService;
    private readonly CourseService _courseService;

    public CourseServiceTests()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockCacheService = new Mock<ICacheService>();
        _courseService = new CourseService(_mockCourseRepository.Object, _mockMapper.Object, _mockCacheService.Object);
    }

    [Fact]
    public async Task GetAllCoursesAsync_ShouldReturnCachedCourses_WhenCacheExists()
    {
        // Arrange
        var cachedCourses = new List<CourseDto>
        {
            new CourseDto { CourseId = 1, CourseName = "Test Course", SemOrYear = "Semester", TotalSemOrYear = 8 }
        };

        // Mock cache hit using the variable approach
        var outValue = cachedCourses as IEnumerable<CourseDto>;
        _mockCacheService.Setup(x => x.TryGetValue<IEnumerable<CourseDto>>("AllCourses", out outValue))
            .Returns(true);

        // Act
        var result = await _courseService.GetAllCoursesAsync();

        // Assert
        Assert.Equal(cachedCourses, result);
        _mockCourseRepository.Verify(x => x.GetAllAsync(), Times.Never);
    }

    [Fact]
    public async Task GetAllCoursesAsync_ShouldFetchFromRepositoryAndCache_WhenCacheDoesNotExist()
    {
        // Arrange
        var courses = new List<Course>
        {
            new Course { CourseId = 1, CourseName = "Test Course", SemOrYear = "Semester", TotalSemOrYear = 8 }
        };

        var courseDtos = new List<CourseDto>
        {
            new CourseDto { CourseId = 1, CourseName = "Test Course", SemOrYear = "Semester", TotalSemOrYear = 8 }
        };

        // Mock cache miss using the variable approach
        IEnumerable<CourseDto>? outValue = null;
        _mockCacheService.Setup(x => x.TryGetValue<IEnumerable<CourseDto>>("AllCourses", out outValue))
            .Returns(false);

        _mockCourseRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(courses);
        _mockMapper.Setup(x => x.Map<IEnumerable<CourseDto>>(courses)).Returns(courseDtos);

        // Act
        var result = await _courseService.GetAllCoursesAsync();

        // Assert
        Assert.Equal(courseDtos, result);
        _mockCourseRepository.Verify(x => x.GetAllAsync(), Times.Once);
        _mockCacheService.Verify(x => x.Set<IEnumerable<CourseDto>>("AllCourses", courseDtos, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public async Task CreateCourseAsync_ShouldThrowException_WhenCourseNameAlreadyExists()
    {
        // Arrange
        var createCourseDto = new CreateCourseDto
        {
            CourseName = "Existing Course",
            SemOrYear = "Semester",
            TotalSemOrYear = 8
        };

        var existingCourse = new Course
        {
            CourseId = 1,
            CourseName = "Existing Course",
            SemOrYear = "Semester",
            TotalSemOrYear = 8
        };

        _mockCourseRepository.Setup(x => x.GetByNameAsync("Existing Course")).ReturnsAsync(existingCourse);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => 
            await _courseService.CreateCourseAsync(createCourseDto));
    }

    [Fact]
    public async Task CreateCourseAsync_ShouldCreateCourseAndInvalidateCache_WhenCourseNameIsUnique()
    {
        // Arrange
        var createCourseDto = new CreateCourseDto
        {
            CourseName = "New Course",
            SemOrYear = "Semester",
            TotalSemOrYear = 8
        };

        var course = new Course
        {
            CourseId = 1,
            CourseName = "New Course",
            SemOrYear = "Semester",
            TotalSemOrYear = 8
        };

        var courseDto = new CourseDto
        {
            CourseId = 1,
            CourseName = "New Course",
            SemOrYear = "Semester",
            TotalSemOrYear = 8
        };

        _mockCourseRepository.Setup(x => x.GetByNameAsync("New Course")).ReturnsAsync((Course?)null);
        _mockMapper.Setup(x => x.Map<Course>(createCourseDto)).Returns(course);
        _mockCourseRepository.Setup(x => x.AddAsync(course)).ReturnsAsync(course);
        _mockMapper.Setup(x => x.Map<CourseDto>(course)).Returns(courseDto);

        // Act
        var result = await _courseService.CreateCourseAsync(createCourseDto);

        // Assert
        Assert.Equal(courseDto, result);
        _mockCourseRepository.Verify(x => x.AddAsync(course), Times.Once);
        _mockCacheService.Verify(x => x.Remove("AllCourses"), Times.Once);
    }
}