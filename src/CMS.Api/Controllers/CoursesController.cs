using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetById(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        
        if (course == null)
            return NotFound();
            
        return Ok(course);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<CourseDto>> GetByName(string name)
    {
        var course = await _courseService.GetCourseByNameAsync(name);
        
        if (course == null)
            return NotFound();
            
        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> Create(CreateCourseDto createCourseDto)
    {
        var createdCourse = await _courseService.CreateCourseAsync(createCourseDto);
        return CreatedAtAction(nameof(GetById), new { id = createdCourse.CourseId }, createdCourse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateCourseDto updateCourseDto)
    {
        try
        {
            await _courseService.UpdateCourseAsync(id, updateCourseDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}