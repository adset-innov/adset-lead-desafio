using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entidades;

public partial class Pacote
{
    [Key]
    [Column("ID")]
    public int Id { get; private set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; private set; } = null!;

    [Column("PortalID")]
    public int PortalId { get; private set; }

    [ForeignKey("PortalId")]
    [InverseProperty("Pacote")]
    public virtual Portal Portal { get; private set; } = null!;

    [InverseProperty("Pacote")]
    [JsonIgnore]
    public virtual ICollection<RelacaoVeiculoPacotePortal> RelacaoVeiculoPacotePortal { get; private set; } = new List<RelacaoVeiculoPacotePortal>();



}