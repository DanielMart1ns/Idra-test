using Leads.Model;
using Microsoft.AspNetCore.Mvc;
using LeadsCrud.Service;

namespace Leads.Controller;

[ApiController]
[Route("api/lead")]
public class LeadController : ControllerBase
{
    private readonly LeadsCrudService _crudService;
    private readonly ILogger<LeadController> _logger;

    public LeadController(LeadsCrudService crudService, ILogger<LeadController> logger)
    {
        _crudService = crudService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterLead([FromBody] Lead leadData)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _logger.LogInformation("Adding lead to database: CNPJ {Cnpj}", leadData.Cnpj);
            
            var success = await _crudService.AddLeadToDb(leadData);
            
            if (!success)
            {
                _logger.LogWarning("Failed to add lead: CNPJ {Cnpj}", leadData.Cnpj);
                return StatusCode(500, "Error adding lead to database");
            }

            _logger.LogInformation("Lead successfully added: CNPJ {Cnpj}", leadData.Cnpj);
            return StatusCode(201, "Lead successfully registered");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering lead: CNPJ {Cnpj}", leadData.Cnpj);
            return StatusCode(500, "Internal error while processing request");
        }
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetLeads()
    {
        try
        {
            List<Lead> leads = await _crudService.GetAllLeads();

            if (leads == null || !leads.Any())
                return NotFound("No leads found");

            return StatusCode(200, leads);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "Error retrieving leads");
            return StatusCode(500, "Internal error while retrieving leads");
        }
    }
}