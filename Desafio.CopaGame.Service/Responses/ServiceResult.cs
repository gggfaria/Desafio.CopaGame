using Desafio.CopaGame.Service.DTOs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Desafio.CopaGame.Service.Responses
{
    public class ServiceResult
    {
        public ServiceResult(string mensagem, int statusCode, bool sucesso = false,
            ICollection<ValidacaoResponseDTO> erros = null)
        {
            Mensagem = mensagem;
            StatusCode = statusCode;
            Sucesso = sucesso;
            Erros = erros;
        }

        [JsonPropertyName("mensagem")]
        public string Mensagem { get; protected set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; protected set; }

        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; protected set; }

        [JsonPropertyName("erros")]
        public ICollection<ValidacaoResponseDTO> Erros { get; protected set; }
    }

    public class ServiceResult<TEntity> : ServiceResult where TEntity : class
    {
        public ServiceResult(string mensagem, int statusCode, bool sucesso = false,
                                TEntity dados = null, ICollection<ValidacaoResponseDTO> erros = null)
            : base(mensagem, statusCode, sucesso, erros)
        {
            Dados = dados;
        }

        [JsonPropertyName("dados")]
        public TEntity Dados { get; private set; }

    }
}
