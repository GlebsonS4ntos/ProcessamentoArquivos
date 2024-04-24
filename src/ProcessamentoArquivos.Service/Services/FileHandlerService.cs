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
            var files = Directory.GetFiles(_config["PathArchive"], "*.txt");
            var clientesDto = new List<ClienteDto>();

            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var info = line.Split('|');
                    if (info.Length > 2)
                    {
                        //retornar uma exception sobre o formato do aquivo
                    }

                    var clienteDto = new ClienteDto() { Nome = info[0], Cpf = info[1] };

                    await ValidarAsync(clienteDto);
                    clientesDto.Add(clienteDto);
                }
            }
            var e =_mapper.Map<List<Cliente>>(clientesDto);
            await _repository.BulkInsertAsync(e);
        }

        private async Task ValidarAsync(ClienteDto dto)
        {
            var validator = new ClienteValidator();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                //gerar exception e adicionar as mensagens de erro
            }
        }
    }
}