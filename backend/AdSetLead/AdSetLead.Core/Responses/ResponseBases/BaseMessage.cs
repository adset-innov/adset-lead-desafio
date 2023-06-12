
using AdSetLead.Core.Responses.Enums;
using AdSetLead.Core.Responses.Interface;
using AdSetLead.Core.Responses.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdSetLead.Core.Responses.ResponseBases
{
    public class BaseMessage : IBaseMessage
    {
        #region Properties
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Reflete se a lista de mensagens tiver alguma mensagem
        /// </summary>
        public bool HasAnyMessages { get { return Messages.Count > 0; } }

        /// <summary>
        /// Reflete se a lista de mensagens tiver alguma mensagem de erro
        /// </summary>
        public bool HasErrorMessages { get { return HasMessageType(MessageTypeEnum.Error); } }

        /// <summary>
        /// Reflete se a lista de mensagens tiver alguma mensagem de exceção
        /// </summary>
        public bool HasExceptionMessages { get { return HasMessageType(MessageTypeEnum.Exception); } }

        /// <summary>
        /// Reflete se a lista de mensagens tiver alguma mensagem de informaçao
        /// </summary>
        public bool HasInformationMessages { get { return HasMessageType(MessageTypeEnum.Info); } }

        /// <summary>
        /// Reflete se a lista de mensagens tiver alguma mensagem de validação
        /// </summary>
        public bool HasValidationMessages { get { return HasMessageType(MessageTypeEnum.Validation); } }

        /// <summary>
        /// Reflete se a lista de mensagens tiver alguma mensagem de aviso (warning)
        /// </summary>
        public bool HasWarningMessage { get { return HasMessageType(MessageTypeEnum.Warning); } }
        #endregion

        #region Public Method
        /// <summary>
        /// Adicionar mensagem de erro ao objeto BaseResponse.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public BaseMessage AddErrorMessage(string code, string text)
        {
            Messages.Add(new Message(MessageTypeEnum.Error, code, text));
            return this;
        }

        /// <summary>
        /// Adicionar mensagem de exceção ao objeto BaseResponse.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public BaseMessage AddExceptionMessage(string code, string text)
        {
            Messages.Add(new Message(MessageTypeEnum.Exception, code, text));

            return this;
        }

        /// <summary>
        /// Adicionar mensagem de informação ao objeto BaseResponse
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public BaseMessage AddInfoMessage(string code, string text)
        {
            Messages.Add(new Message(MessageTypeEnum.Info, code, text));

            return this;
        }

        /// <summary>
        /// Adicionar mensagem de validação à coleção Messages
        /// </summary>
        /// <param name="status"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseMessage AddValidationMessage(string code, string text)
        {
            Messages.Add(new Message(MessageTypeEnum.Validation, code, text));

            return this;
        }

        /// <summary>
        /// Adicionar mensagem de aviso ao objeto BaseResponse
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public BaseMessage AddWarningMessage(string code, string text)
        {
            Messages.Add(new Message(MessageTypeEnum.Warning, code, text));

            return this;
        }

        public virtual void Merge(IBaseMessage baseResponse)
        {
            this.Messages.AddRange(baseResponse.Messages);
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Verifique se mensagens foram adicionadas no objeto.
        /// </summary>
        /// <param name="messageType"></param>
        /// <returns></returns>
        private bool HasMessageType(MessageTypeEnum messageType)
        {
            return Messages.Any(item => item.MessageType == messageType);
        }
        #endregion
    }
}
