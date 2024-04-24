using FluentValidation;
using ProcessamentoArquivos.Service.Dtos;

namespace ProcessamentoArquivos.Service.Validations
{
    public class ClienteValidator : AbstractValidator<ClienteDto>
    {
        public ClienteValidator() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Nome do cliente nulo.")
                .NotNull().WithMessage("Nome do cliente vazio.")
                .MaximumLength(200).WithMessage("Nome do cliente possue mais de 200 caracteres");

            RuleFor(c => c.Cpf)
                .NotNull().WithMessage("CPF do cliente nulo.")
                .NotEmpty().WithMessage("CPF do cliente vazio.")
                .Matches("^[0-9]{11}$").WithMessage("CPF do cliente invalido.")
                .Must(CpfValidator).WithMessage("CPF do cliente invalido.");
        }

        private bool CpfValidator(string cpf)
        {
            try
            {
                var arrayCpf = cpf.ToCharArray();
                var arrayCpfInt = new int[arrayCpf.Length];

                for(int i = 0; i < arrayCpf.Length; i++)
                {
                    arrayCpfInt[i] = int.Parse(arrayCpf[i].ToString());
                }

                var arrayMultiplicacaoPrimeiroVerificador = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var arrayMultiplicacaoSegundoVerificador = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                var resultadoAdicao = 0;

                //como o array de multiplicacao tem tamanho 9 vai pegar os 9 primeiros do array de cpf
                for (int i = 0; i < arrayMultiplicacaoPrimeiroVerificador.Length; i++)
                {
                    var resultadoMultiplicacao = arrayMultiplicacaoPrimeiroVerificador[i] * arrayCpfInt[i];
                    resultadoAdicao += resultadoMultiplicacao;
                }

                var primeiroVerificadorEsperado = (resultadoAdicao % 11) > 1 ? 11 - (resultadoAdicao % 11) : 0;

                if (primeiroVerificadorEsperado != arrayCpfInt[9]) //Comparação pra ver se o 1 digito bate com o que foi calculado
                {
                    return false;
                }

                resultadoAdicao = 0;

                for (int i = 0; i < arrayMultiplicacaoSegundoVerificador.Length; i++)
                {
                    resultadoAdicao += arrayMultiplicacaoSegundoVerificador[i] * arrayCpfInt[i];
                }

                var segundoVerificadorEsperado = (resultadoAdicao % 11) > 1 ? 11 - (resultadoAdicao % 11) : 0;

                if (segundoVerificadorEsperado != arrayCpfInt[10]) //Comparação pra ver se o 2 digito bate com o que foi calculado
                {
                    return false;
                }
                return true;

            } catch(Exception ex) { 
               throw new Exception(ex.Message);
            }
        }
    }
}
