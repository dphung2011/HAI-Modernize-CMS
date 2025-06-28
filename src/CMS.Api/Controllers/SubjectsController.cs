using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll()
    {
        var subjects = await _subjectService.GetAllSubjectsAsync();
        return Ok(subjects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDto>> GetById(int id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        
        if (subject == null)
            return NotFound();
            
        return Ok(subject);
    }

    [HttpGet("course/{courseName}")]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetByCourse(string courseName)
    {
        var subjects = await _subjectService.GetSubjectsByCourseAsync(courseName);
        return Ok(subjects);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<SubjectDto>> GetByName(string name)
    {
        var subject = await _subjectService.GetSubjectByNameAsync(name);
        
        if (subject == null)
            return NotFound();
            
        return Ok(subject);
    }

    [HttpPost]
    public async Task<ActionResult<SubjectDto>> Create(CreateSubjectDto createSubjectDto)
    {
        var createdSubject = await _subjectService.CreateSubjectAsync(createSubjectDto);
        return CreatedAtAction(nameof(GetById), new { id = createdSubject.SubjectId }, createdSubject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateSubjectDto updateSubjectDto)
    {
        try
        {
            await _subjectService.UpdateSubjectAsync(id, updateSubjectDto);
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
            await _subjectService.DeleteSubjectAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}