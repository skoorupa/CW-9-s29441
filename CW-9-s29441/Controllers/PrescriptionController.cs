using CW_9_s29441.DTOs;
using CW_9_s29441.Exceptions;
using CW_9_s29441.Models;
using CW_9_s29441.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_9_s29441.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateDto prescriptionCreateDto)
    {
        try
        {
            var prescription = await service.CreatePrescriptionAsync(prescriptionCreateDto);
            return CreatedAtAction(nameof(CreatePrescription), prescriptionCreateDto);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (IncorrectDateException e)
        {
            return BadRequest(e.Message);
        }
    }
}