using System.ComponentModel;

namespace adset_api.Enum
{
    public enum ETipoPacote
    {
        [Description("Não Informado")]
        NaoInformado = 0,
        [Description("Bronze")]
        Bronze = 1,
        [Description("Diamante")]
        Diamante = 2,
        [Description("Platinum")]
        Platinum = 3,
        [Description("Básico")]
        Basico = 4
    }
}
