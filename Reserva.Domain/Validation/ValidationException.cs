using System;
using System.Collections.Generic;
using System.Text;

namespace Reserva.Domain.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(List<BusinessRule> brokenRules) : base(BuildBrokenRulesMessage(brokenRules))
        {
            BrokenRules = brokenRules;
        }

        public List<BusinessRule> BrokenRules { get; set; }

        private static string BuildBrokenRulesMessage(IEnumerable<BusinessRule> brokenRules)
        {
            var brokenRulesBuilder = new StringBuilder();
            foreach (var businessRule in brokenRules) brokenRulesBuilder.AppendLine(businessRule.RuleDescription);
            return brokenRulesBuilder.ToString();
        }
    }

    public static class GenericExceptions
    {
        public static readonly ValidationException NaoEncontrado =
            new ValidationException("Registro não encontrado");

        public static readonly ValidationException JaCadastrado =
            new ValidationException("Regitro já cadastrado.");

        public static readonly ValidationException CodigoInvalido =
            new ValidationException("Código invalido para entidade.");

        public static readonly ValidationException FalhaGenerica =
            new ValidationException("Falha ao processar requisição.");

        public static readonly ValidationException SenhaInvalida =
            new ValidationException("Senha inválida");
    }
}