using AutoMapper;
using Microsoft.Extensions.Configuration;
using ProcessamentoArquivos.Domain.Entities;
using ProcessamentoArquivos.Infraestructure.Interfaces;
using ProcessamentoArquivos.Service.Dtos;
using ProcessamentoArquivos.Service.Interfaces;
using ProcessamentoArquivos.Service.Validations;

namespace ProcessamentoArquivos.Service.Services
{
    public class FileHandlerService : IFileHandlerService
    {
        private readonly IRepositoryCliente _repository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public FileHandlerService(IRepositoryCliente repository, IConfiguration config, IMapper mapper)
        {
            _repository = repository;
            _config = config;
            _mapper = mapper;
        }

        public async Task ReadFilesAsync()
        {
            try
            {
                var files = Directory.GetFiles(_config["PathArchive"], "*.txt");
                var clientesDto = new List<ClienteDto>();

                foreach (var file in files)
                {
                    string[] lines = File.ReadAllLines(file);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var info = lines[i].Split('|');
                        if (info.Length > 2)
                        {
                            throw new Exception($"Cliente Possue mais de 2 Propriedades, isso ocorre inicialmente na linha {i+1} no arquivo {file}.");
                        }

                        var clienteDto = new ClienteDto() { Nome = info[0], Cpf = info[1] };

                        // i+1 indica a linha que esta sendo lida, ja que o indice comeca em 0
                        await ValidarAsync(clienteDto, (i + 1), file.ToString());
                        clientesDto.Add(clienteDto);
                    }
                }
                var e = _mapper.Map<List<Cliente>>(clientesDto);
                await _repository.BulkInsertAsync(e);
            } catch (Exception ex) {
                throw ex;
            }
        }

        private async Task ValidarAsync(ClienteDto dto, int numberLine, string nameFile)
        {
            var validator = new ClienteValidator();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                var mensagemErro = string.Join(", ", erros);
                throw new Exception($"Foram encontrados erros de validacoes ao tentar adicionar o registro da linha: {numberLine} do arquivo {nameFile} com os erros: {mensagemErro}.");
            }
        }
    }
}