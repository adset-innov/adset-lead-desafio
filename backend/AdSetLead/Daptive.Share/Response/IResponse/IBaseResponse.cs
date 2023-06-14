
using Daptive.Share.Model;
using System.Collections.Generic;

namespace Daptive.Share.Response.IResponse
{
    public interface IBaseResponse
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
        /// Possui mensagens de validação?
        /// </summary>
        bool HasValidationMessages { get; }

        /// <summary>
        /// Quantidade de itens
        /// </summary>
        int TotalAvailableItems { get; set; }

        List<Message> Messages { get; set; }

        /// <summary>
        /// Metodo para adicionar mensagens de erro
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseResponse AddErrorMessage(string text, string code = "");

        /// <summary>
        /// Metodo para adicionar mensagens de exceção
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        BaseResponse AddExceptionMessage(string text, string code = "");

        /// <summary>
        /// Método para adicionar mensagens de informação
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseResponse AddInfoMessage(string text, string code = "");

        /// <summary>
        /// Method to add Validation messages
        /// </summary>
        /// <param name="status"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseResponse AddValidationMessage(string text, string code = "");

        /// <summary>
        /// Metodo para adicionar mensagens de validação
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        BaseResponse AddWarningMessage(string text, string code = "");
    }
}
