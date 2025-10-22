using API.Dtos.Foto;
using API.Entidades;
using API.Exceptions;
using API.Repositorios.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementacoes
{
    public class FotoService : IFotoService
    {
        private readonly IFotoRepository _repository;
        private readonly IMapper _mapper;
        public FotoService(IFotoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CadastrarFotos(int veiculoId, List<IFormFile> fotos)
        {

            string pastaVeiculo = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "veiculos", veiculoId.ToString());

            if (!Directory.Exists(pastaVeiculo))
            {
                Directory.CreateDirectory(pastaVeiculo);
            }

            foreach (IFormFile fotoFile in fotos)
            {
                string extensao = Path.GetExtension(fotoFile.FileName);
                string nomeArquivoFisico = $"{Guid.NewGuid()}{extensao}";
                string caminhoCompletoArquivo = Path.Combine(pastaVeiculo, nomeArquivoFisico);
                string pathHttp = string.Concat("veiculos/", veiculoId, "/", nomeArquivoFisico);

                using (FileStream stream = new FileStream(caminhoCompletoArquivo, FileMode.Create))
                {
                    await fotoFile.CopyToAsync(stream);
                }

                Foto fotoEntity = new Foto(
                    veiculoId,
                    fotoFile.FileName,
                    pathHttp
                );

                await _repository.InserirFotoAsync(fotoEntity);

            }        
        }

        public async Task DeletarFotoAsync(int idFoto)
        {
            Foto fotoAtual = await _repository.ObterFotoPeloIdAsync(idFoto);

            if (fotoAtual == null)
                throw new NotFoundException("Foto não encontrada");


            await _repository.DeletarFotoAsync(fotoAtual);

            string caminhoFisicoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fotoAtual.Path);

            if (File.Exists(caminhoFisicoCompleto))
            {
                File.Delete(caminhoFisicoCompleto);
            }

        }


        public async Task<FotoDto> EditarFotoVeiculoAsync(int idFoto, IFormFile foto)
        {
            Foto fotoAtual = await _repository.ObterFotoPeloIdAsync(idFoto);
            if (fotoAtual == null)
                throw new NotFoundException("Foto não encontrada");

            string caminhoFisicoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fotoAtual.Path);

            await using (Stream novoConteudoStream = foto.OpenReadStream())
            {
                await using (FileStream fileStream = new FileStream(caminhoFisicoCompleto, FileMode.Create))
                {
                    await novoConteudoStream.CopyToAsync(fileStream);
                }
            }

            fotoAtual.SetArquivo(foto.FileName);

            await _repository.EditarFotoAsync(fotoAtual);

            FotoDto fotoDto = _mapper.Map<FotoDto>(fotoAtual);
            return fotoDto;


        }
    }
}
