using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leads.Model
{
    [Table("cadastrolead")] // nome da tabela no banco
    public class Lead
    {
        // [Column("id")]
        // public long Id { get; set; }

        [Key]
        [Column("cnpj")]
        public required string Cnpj { get; set; }

        [Column("razaosocial")]
        public required string RazaoSocial { get; set; }

        [Column("cep")]
        public required string Cep { get; set; }

        [Column("endereco")]
        public required string Endereco { get; set; }

        [Column("numero")]
        public required int Numero { get; set; }

        [Column("complemento")]
        public string? Complemento { get; set; }

        [Column("bairro")]
        public required string Bairro { get; set; }

        [Column("cidade")]
        public required string Cidade { get; set; }

        [Column("estado")]
        public required string Estado { get; set; }
    }
}