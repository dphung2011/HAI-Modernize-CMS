using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MarksController : ControllerBase
{
    private readonly IMarkService _markService;

    public MarksController(IMarkService markService)
    {
        _markService = markService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarkDto>>> GetAll()
    {
        var marks = await _markService.GetAllMarksAsync();
        return Ok(marks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MarkDto>> GetById(int id)
    {
        var mark = await _markService.GetMarkByIdAsync(id);
        
        if (mark == null)
            return NotFound();
            
        return Ok(mark);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<MarkDto>> GetByStudentId(int studentId)
    {
        var mark = await _markService.GetMarkByStudentIdAsync(studentId);
        
        if (mark == null)
            return NotFound();
            
        return Ok(mark);
    }

    [HttpGet("course/{courseName}")]
    public async Task<ActionResult<IEnumerable<MarkDto>>> GetByCourse(string courseName)
    {
        var marks = await _markService.GetMarksByCourseAsync(courseName);
        return Ok(marks);
    }

    [HttpPost]
    public async Task<ActionResult<MarkDto>> Create(CreateMarkDto createMarkDto)
    {
        try
        {
            var createdMark = await _markService.CreateMarkAsync(createMarkDto);
            return CreatedAtAction(nameof(GetById), new { id = createdMark.MarkId }, createdMark);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateMarkDto updateMarkDto)
    {
        try
        {
            await _markService.UpdateMarkAsync(id, updateMarkDto);
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
            await _markService.DeleteMarkAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}