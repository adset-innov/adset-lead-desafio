
using Daptive.Share.Enums;

namespace Daptive.Share.Model
{
    public class Message
    {
        public Message()
        {
        }

        public Message(MessageTypeEnum messageType, string code, string text)
        {
            MessageType = messageType;
            Code = code;
            Text = text;
        }

        public MessageTypeEnum MessageType { get; set; } = MessageTypeEnum.None;

        /// <summary>
        /// Um código de mensagem exclusivo para ser usado para localizar facilmente no código-fonte onde uma mensagem foi criada,
        /// mas também para oferecer suporte à internacionalização
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// O texto da mensagem associado ao código.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Retorne uma versão stringificada desta classe
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Type:{MessageType}, code:{Code}, Message:{Text}";
        }

        /// <summary>
        /// Metodo de conveniência para criar mensagem de validação.
        /// Message type if automatically set to <see cref="MessageTypeEnum.Validation"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Message CreateValidationMessage(string code, string text = null)
        {
            return new Message(MessageTypeEnum.Error, code, text);
        }

        /// <summary>
        /// Método de conveniência para criar mensagem de erro.
        /// Tipo de mensagem se definido automaticamente como <see cref="MessageTypeEnum.Error"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Message CreateErrorMessage(string code, string text = null)
        {
            return new Message(MessageTypeEnum.Error, code, text);
        }

        /// <summary>
        /// Método de conveniência para criar mensagem de aviso.
        /// Tipo de mensagem se definido automaticamente como<see cref="MessageTypeEnum.Warning"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Message CreateWarningMessage(string code, string text = null)
        {
            return new Message(MessageTypeEnum.Warning, code, text);
        }

        /// <summary>
        /// Método de conveniência para criar mensagem de informação.
        /// Tipo de mensagem se definido automaticamente como <see cref="MessageTypeEnum.Info"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Message CreateInfoMessage(string code, string text = null)
        {
            return new Message(MessageTypeEnum.Info, code, text);
        }
    }
}
