
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entidades;

public partial class Veiculo
{
    [Key]
    [Column("ID")]
    public int Id { get; private set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Marca { get; private set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Modelo { get; private set; } = null!;

    public int Ano { get; private set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Placa { get; private set; } = null!;

    public int? Km { get; private set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Cor { get; private set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Preco { get; private set; }

    [InverseProperty("Veiculo")]
    public virtual ICollection<Foto> Foto { get; private set; } = new List<Foto>();

    [InverseProperty("Veiculo")]
    public virtual ICollection<RelacaoVeiculoOpcional> RelacaoVeiculoOpcional { get; private set; } = new List<RelacaoVeiculoOpcional>();

    [InverseProperty("Veiculo")]
    public virtual ICollection<RelacaoVeiculoPacotePortal> RelacaoVeiculoPacotePortal { get; private set; } = new List<RelacaoVeiculoPacotePortal>();




}
