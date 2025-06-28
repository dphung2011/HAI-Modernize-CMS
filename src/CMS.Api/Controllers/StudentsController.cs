using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers;

/// <summary>
/// Controller for managing students
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentsController"/> class.
    /// </summary>
    /// <param name="studentService">The student service.</param>
    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Gets all students
    /// </summary>
    /// <returns>A list of all students</returns>
    /// <response code="200">Returns the list of students</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    /// <summary>
    /// Gets a student by ID
    /// </summary>
    /// <param name="id">The student ID</param>
    /// <returns>The student with the specified ID</returns>
    /// <response code="200">Returns the student</response>
    /// <response code="404">If the student does not exist</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<StudentDto>> GetById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        
        if (student == null)
            return NotFound();
            
        return Ok(student);
    }

    /// <summary>
    /// Gets students by course name
    /// </summary>
    /// <param name="courseName">The course name</param>
    /// <returns>A list of students enrolled in the specified course</returns>
    /// <response code="200">Returns the list of students</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpGet("course/{courseName}")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetByCourse(string courseName)
    {
        var students = await _studentService.GetStudentsByCourseAsync(courseName);
        return Ok(students);
    }

    /// <summary>
    /// Creates a new student
    /// </summary>
    /// <param name="createStudentDto">The student to create</param>
    /// <returns>The created student</returns>
    /// <response code="201">Returns the newly created student</response>
    /// <response code="400">If the student data is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPost]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<StudentDto>> Create(CreateStudentDto createStudentDto)
    {
        var createdStudent = await _studentService.CreateStudentAsync(createStudentDto);
        return CreatedAtAction(nameof(GetById), new { id = createdStudent.StudentId }, createdStudent);
    }

    /// <summary>
    /// Updates an existing student
    /// </summary>
    /// <param name="id">The student ID</param>
    /// <param name="updateStudentDto">The updated student data</param>
    /// <returns>No content</returns>
    /// <response code="204">If the student was successfully updated</response>
    /// <response code="400">If the student data is invalid</response>
    /// <response code="404">If the student does not exist</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, UpdateStudentDto updateStudentDto)
    {
        try
        {
            await _studentService.UpdateStudentAsync(id, updateStudentDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates a student's course
    /// </summary>
    /// <param name="id">The student ID</param>
    /// <param name="courseName">The new course name</param>
    /// <returns>No content</returns>
    /// <response code="204">If the student's course was successfully updated</response>
    /// <response code="400">If the course name is invalid</response>
    /// <response code="404">If the student does not exist</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpPut("{id}/course/{courseName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateCourse(int id, string courseName)
    {
        try
        {
            await _studentService.UpdateStudentCourseAsync(id, courseName);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a student
    /// </summary>
    /// <param name="id">The student ID</param>
    /// <returns>No content</returns>
    /// <response code="204">If the student was successfully deleted</response>
    /// <response code="404">If the student does not exist</response>
    /// <response code="401">If the user is not authenticated</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}