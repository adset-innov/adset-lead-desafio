using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entidades;

public partial class Opcional
{
    [Key]
    [Column("ID")]
    public int Id { get; private set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Descricao { get; private set; } = null!;

    [JsonIgnore]
    [InverseProperty("Opcional")]
    public virtual ICollection<RelacaoVeiculoOpcional> RelacaoVeiculoOpcional { get; private set; } = new List<RelacaoVeiculoOpcional>();


}