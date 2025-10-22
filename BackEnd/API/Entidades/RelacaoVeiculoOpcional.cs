using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entidades;

public partial class RelacaoVeiculoOpcional
{
    [Key]
    [Column("ID")]
    public int Id { get; private set; }

    [Column("VeiculoID")]
    public int VeiculoId { get; private set; }

    [Column("OpcionalID")]
    public int OpcionalId { get; private set; }

    [ForeignKey("OpcionalId")]
    [InverseProperty("RelacaoVeiculoOpcional")]
    public virtual Opcional Opcional { get; private set; } = null!;

    [JsonIgnore]
    [ForeignKey("VeiculoId")]
    [InverseProperty("RelacaoVeiculoOpcional")]
    public virtual Veiculo Veiculo { get; private set; } = null!;

    public RelacaoVeiculoOpcional(int opcionalId)
    {
        OpcionalId = opcionalId;
    }
}
