
using Daptive.Share.Enums;

namespace Daptive.Share.Model.IModel
{
    public interface IMessage
    {
        /// <summary>
        /// Codigo da Mensagem
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Texto da mensagem
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Tipo da mensagem
        /// </summary>
        MessageTypeEnum MessageType { get; set; }
    }
}
