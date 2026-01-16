using Leads.Controller;
using Leads.Model;
using Microsoft.EntityFrameworkCore;
using MinhaApiRest.Data;

namespace LeadsCrud.Service;

public class LeadsCrudService
{
    private readonly AppDbContext _db;
    private readonly ILogger<LeadController> _logger;

    public LeadsCrudService(AppDbContext db, ILogger<LeadController> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> AddLeadToDb(Lead leadData)
    {
        try
        {
            _db.CadastroLead.Add(leadData);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error to add lead: {ex}", ex);
            return false;
        }
    }

    public async Task<List<Lead>> GetAllLeads()
    {
        return await _db.CadastroLead.ToListAsync();
    }

    public async Task<Lead?> GetLeadByCnpj(string cnpj)
    {
        return await _db.CadastroLead.FindAsync(cnpj);
    }

    public async Task<bool> UpdateLeadFromDb(Lead existingLead, Lead leadUpdated)
    {
        try
        {
            existingLead.RazaoSocial  = leadUpdated.RazaoSocial;
            existingLead.Cep          = leadUpdated.Cep;
            existingLead.Endereco     = leadUpdated.Endereco;
            existingLead.Numero       = leadUpdated.Numero;
            existingLead.Complemento  = leadUpdated.Complemento;
            existingLead.Bairro       = leadUpdated.Bairro;
            existingLead.Cidade       = leadUpdated.Cidade;
            existingLead.Estado       = leadUpdated.Estado;

            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao atualizar lead {Cnpj}: {ex}", existingLead.Cnpj, ex);
            return false;
        }
    }

    public async Task<bool> DeleteFromDb(Lead lead)
    {
        try
        {
            _db.CadastroLead.Remove(lead);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error to delete lead: {ex.Message}");
            return false;
        }        
    }
}