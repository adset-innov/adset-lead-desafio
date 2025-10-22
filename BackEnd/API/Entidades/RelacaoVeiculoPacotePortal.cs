using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

[Index("VeiculoId", "PortalId", Name = "UQ_Veiculo_Portal", IsUnique = true)]
public partial class RelacaoVeiculoPacotePortal
{
    [Key]
    [Column("ID")]
    public int Id { get; private set; }

    [Column("VeiculoID")]
    public int VeiculoId { get; private set; }

    [Column("PacoteID")]
    public int PacoteId { get; private set; }

    [Column("PortalID")]
    public int PortalId { get; private set; }

    [ForeignKey("PacoteId")]
    [InverseProperty("RelacaoVeiculoPacotePortal")]
    public virtual Pacote Pacote { get; private set; } = null!;

    [JsonIgnore]
    [ForeignKey("PortalId")]
    [InverseProperty("RelacaoVeiculoPacotePortal")]
    public virtual Portal Portal { get; private set; } = null!;

    [JsonIgnore]
    [ForeignKey("VeiculoId")]
    [InverseProperty("RelacaoVeiculoPacotePortal")]
    public virtual Veiculo Veiculo { get; private set; } = null!;

    public void SetVeiculoId(int veiculoId)
    {
        if (veiculoId <= 0)
            throw new ArgumentException("O ID do veículo é inválido.", nameof(veiculoId));
        VeiculoId = veiculoId;
    }


    public void SetPacoteId(int pacoteId)
    {
        if (pacoteId <= 0)
            throw new ArgumentException("O ID do pacote é inválido.", nameof(pacoteId));
        PacoteId = pacoteId;
    }

    public void SetPortalId(int portalId)
    {
        if (portalId <= 0)
            throw new ArgumentException("O ID do portal é inválido.", nameof(portalId));
        PortalId = portalId;
    }
}
