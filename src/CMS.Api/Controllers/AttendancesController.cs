using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendancesController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendancesController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAll()
    {
        var attendances = await _attendanceService.GetAllAttendancesAsync();
        return Ok(attendances);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AttendanceDto>> GetById(int id)
    {
        var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
        
        if (attendance == null)
            return NotFound();
            
        return Ok(attendance);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetByStudentId(int studentId)
    {
        var attendances = await _attendanceService.GetAttendancesByStudentIdAsync(studentId);
        return Ok(attendances);
    }

    [HttpGet("date/{date}")]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetByDate(string date)
    {
        var attendances = await _attendanceService.GetAttendancesByDateAsync(date);
        return Ok(attendances);
    }

    [HttpGet("course/{courseName}/subject/{subjectName}")]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetByCourseAndSubject(string courseName, string subjectName)
    {
        var attendances = await _attendanceService.GetAttendancesByCourseAndSubjectAsync(courseName, subjectName);
        return Ok(attendances);
    }

    [HttpPost]
    public async Task<ActionResult<AttendanceDto>> Create(CreateAttendanceDto createAttendanceDto)
    {
        try
        {
            var createdAttendance = await _attendanceService.CreateAttendanceAsync(createAttendanceDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAttendance.AttendanceId }, createdAttendance);
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
            await _attendanceService.DeleteAttendanceAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}