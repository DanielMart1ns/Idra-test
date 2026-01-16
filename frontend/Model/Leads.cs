using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leads.Model
{
    public class Lead
    {
        public required string Cnpj { get; set; }

        public required string RazaoSocial { get; set; }

        public required string Cep { get; set; }

        public required string Endereco { get; set; }

        public required int Numero { get; set; }

        public string? Complemento { get; set; }

        public required string Bairro { get; set; }

        public required string Cidade { get; set; }

        public required string Estado { get; set; }
    }
}