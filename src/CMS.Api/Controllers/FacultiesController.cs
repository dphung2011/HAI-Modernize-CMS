using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FacultiesController : ControllerBase
{
    private readonly IFacultyService _facultyService;

    public FacultiesController(IFacultyService facultyService)
    {
        _facultyService = facultyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FacultyDto>>> GetAll()
    {
        var faculties = await _facultyService.GetAllFacultiesAsync();
        return Ok(faculties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FacultyDto>> GetById(int id)
    {
        var faculty = await _facultyService.GetFacultyByIdAsync(id);
        
        if (faculty == null)
            return NotFound();
            
        return Ok(faculty);
    }

    [HttpGet("subject/{subjectName}")]
    public async Task<ActionResult<IEnumerable<FacultyDto>>> GetBySubject(string subjectName)
    {
        var faculties = await _facultyService.GetFacultiesBySubjectAsync(subjectName);
        return Ok(faculties);
    }

    [HttpPost]
    public async Task<ActionResult<FacultyDto>> Create(CreateFacultyDto createFacultyDto)
    {
        var createdFaculty = await _facultyService.CreateFacultyAsync(createFacultyDto);
        return CreatedAtAction(nameof(GetById), new { id = createdFaculty.FacultyId }, createdFaculty);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateFacultyDto updateFacultyDto)
    {
        try
        {
            await _facultyService.UpdateFacultyAsync(id, updateFacultyDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/subject/{subjectName}")]
    public async Task<IActionResult> UpdateSubject(int id, string subjectName)
    {
        try
        {
            await _facultyService.UpdateFacultySubjectAsync(id, subjectName);
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
            await _facultyService.DeleteFacultyAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}