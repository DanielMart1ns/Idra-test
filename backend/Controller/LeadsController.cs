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
            
            bool success = await _crudService.AddLeadToDb(leadData);
            
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
            _logger.LogInformation("collecting all leads...");

            List<Lead> leads = await _crudService.GetAllLeads();

            if (leads == null || !leads.Any())
                return NotFound("No leads found");

            _logger.LogInformation("all leads collected successfully");
            return StatusCode(200, leads);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "Error retrieving leads");
            return StatusCode(500, "Internal error while retrieving leads");
        }
    }

    [HttpGet("get-lead")]
    public async Task<IActionResult> GetALead([FromBody] string cnpj)
    {
        try
        {
            _logger.LogInformation("Collecting lead {cnpj}...", cnpj);

            Lead? lead = await _crudService.GetLeadByCnpj(cnpj);

            if (lead == null)
                return NotFound("No leads found");

            _logger.LogInformation("Lead collected successfully");
            return StatusCode(200, lead);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "Error retrieving lead");
            return StatusCode(500, "Internal error while retrieving lead");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateLead([FromBody] Lead leadDataUpdated)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _logger.LogInformation("Updating lead {cnpj}...", leadDataUpdated.Cnpj);

            Lead? existingLead = await _crudService.GetLeadByCnpj(leadDataUpdated.Cnpj);

            if (existingLead == null)
                return NotFound("No leads found");
            
            bool succes = await _crudService.UpdateLeadFromDb(existingLead, leadDataUpdated);

            _logger.LogInformation("Lead updated successfully");
            return StatusCode(200, "Lead updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "Error updating lead");
            return StatusCode(500, "Internal error while updating lead");
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteLead([FromBody] string cnpj)
    {
        try
        {
            _logger.LogInformation("collecting lead to be deleted...");
            var lead = await _crudService.GetLeadByCnpj(cnpj);

            if (lead == null)
                return NotFound("No lead found");
            
            bool success = await _crudService.DeleteFromDb(lead);

            if (!success)
            {
                _logger.LogWarning("Failed to delete lead: CNPJ {Cnpj}", cnpj);
                return StatusCode(500, "Error deleting lead to database");
            }

            _logger.LogInformation("Lead successfully deleted: CNPJ {Cnpj}", cnpj);
            return StatusCode(204, "Lead successfully deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting lead: CNPJ {Cnpj}", cnpj);
            return StatusCode(500, "Internal error while processing request");
        }
    }
}