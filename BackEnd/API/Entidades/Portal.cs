using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

public partial class Portal
{
    [Key]
    [Column("ID")]
    public int Id { get; private set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; private set; } = null!;

    [JsonIgnore]
    [InverseProperty("Portal")]
    public virtual ICollection<Pacote> Pacote { get; private set; } = new List<Pacote>();

    [JsonIgnore]
    [InverseProperty("Portal")]
    public virtual ICollection<RelacaoVeiculoPacotePortal> RelacaoVeiculoPacotePortal { get; private set; } = new List<RelacaoVeiculoPacotePortal>();



}
