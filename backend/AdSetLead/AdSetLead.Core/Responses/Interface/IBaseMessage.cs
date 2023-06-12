using AdSetLead.Core.Responses.Models;
using AdSetLead.Core.Responses.ResponseBases;
using System.Collections.Generic;

namespace AdSetLead.Core.Responses.Interface
{
    public interface IBaseMessage
    {
        /// <summary>
        /// Tem alguma mensagem?
        /// </summary>
        bool HasAnyMessages { get; }

        /// <summary>
        /// Tem mensagens de erro?
        /// </summary>
        bool HasErrorMessages { get; }

        /// <summary>
        /// Possui mensagens de informação?
        /// </summary>
        bool HasInformationMessages { get; }

        /// <summary>
        /// Metodo para adicionar mensagens de erro
        /// </summary>
        bool HasValidationMessages { get; }

        List<Message> Messages { get; set; }

        /// <summary>
        /// Metodo para adicionar mensagens de erro
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseMessage AddErrorMessage(string code, string text);

        /// <summary>
        /// Metodo para adicionar mensagens de exceção
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        BaseMessage AddExceptionMessage(string code, string text);

        /// <summary>
        /// Método para adicionar mensagens de informação
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseMessage AddInfoMessage(string code, string text);

        /// <summary>
        /// Method to add Validation messages
        /// </summary>
        /// <param name="status"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseMessage AddValidationMessage(string code, string text);

        /// <summary>
        /// Metodo para adicionar mensagens de validação
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseMessage AddWarningMessage(string code, string text);
    }
}
