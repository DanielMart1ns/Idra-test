using Leads.Model;
using Microsoft.EntityFrameworkCore;
using MinhaApiRest.Data;

namespace LeadsCrud.Service;

public class LeadsCrudService
{
    private readonly AppDbContext _db;

    public LeadsCrudService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> AddLeadToDb(Lead data)
    {
        try
        {
            _db.CadastroLead.Add(data);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar lead: {ex.Message}");
            return false;
        }
    }

    public async Task<List<Lead>> GetAllLeads()
    {
        return await _db.CadastroLead.ToListAsync();
    }

    // public async Task<Lead> 
}