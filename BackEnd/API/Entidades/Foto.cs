using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entidades;



public partial class Foto
{
    [Key]
    [Column("ID")]
    public int Id { get; private set; }

    [Column("VeiculoID")]
    public int VeiculoId { get; private set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Arquivo { get; private set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Path { get; private set; } = null!;

    [ForeignKey("VeiculoId")]
    [InverseProperty("Foto")]
    [JsonIgnore]
    public virtual Veiculo Veiculo { get; private set; } = null!;

    public Foto(int veiculoId, string arquivo, string path)
    {
       
        if (veiculoId <= 0)
            throw new ArgumentException("O ID do veículo é inválido.", nameof(veiculoId));

        if (string.IsNullOrWhiteSpace(arquivo))
            throw new ArgumentNullException(nameof(arquivo), "O nome do arquivo não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path), "O caminho do arquivo não pode ser vazio.");

        VeiculoId = veiculoId;
        Arquivo = arquivo;
        Path = path;
    }

    public void SetVeiculoId(int veiculoId)
    {
        if (veiculoId <= 0)
            throw new ArgumentException("O ID do veículo é inválido.", nameof(veiculoId));

        VeiculoId = veiculoId;
    }


    public void SetArquivo(string arquivo)
    {
        if (string.IsNullOrWhiteSpace(arquivo))
            throw new ArgumentNullException(nameof(arquivo), "O nome do arquivo não pode ser vazio.");

        Arquivo = arquivo;
    }


    public void SetPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path), "O caminho do arquivo não pode ser vazio.");

        Path = path;
    }
}
