using Desafio.CopaGame.Service.DTOs;
using System.Collections.Generic;
using System.Net;

namespace Desafio.CopaGame.Service.Responses
{
    public static class ServiceResultFactory
    {
        public static ServiceResult Ok(string mensagem)
        {
            return new ServiceResult(
                mensagem,
                (int)HttpStatusCode.OK,
                sucesso: true
            );
        }

        public static ServiceResult BadRequest(ValidacaoResponseDTO validacao, string mensagem)
        {
            return new ServiceResult(
                mensagem,
                (int)HttpStatusCode.BadRequest,
                erros: new List<ValidacaoResponseDTO>() { validacao }
            );
        }

        public static ServiceResult BadRequest(ICollection<ValidacaoResponseDTO> validacao, string mensagem)
        {
            return new ServiceResult(
                mensagem,
                (int)HttpStatusCode.BadRequest,
                erros: validacao
            );
        }

    }

    public static class ServiceResultFactory<T> where T : class
    {
        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T>(
                mensagem: null,
                (int)HttpStatusCode.OK,
                sucesso: true,
                data
            );
        }

    }
}
