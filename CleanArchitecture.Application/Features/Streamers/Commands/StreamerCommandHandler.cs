using AutoMapper;
using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class StreamerCommandHandler: IRequestHandler<StreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger <StreamerCommandHandler> _logger;


        public StreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, IEmailService emailService, ILogger<StreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(StreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);

            if (streamerEntity == null) return 0;

            var streamer = await _streamerRepository.AddAsync(streamerEntity);

            _logger.LogInformation($"Streamer {streamer.Id} fue registrado exitosamente");

            await SendEmail(streamerEntity);

            return streamer.Id;

        }

        private async Task SendEmail(Streamer streamer)
        {
            if (streamer == null) throw new ArgumentNullException(nameof(streamer));

            var email = new Email
            {
                To = "leandro.gordillo@gmail.com",
                Body = "La compañia de streamer se creó correctamente",
                Subject = "Mensaje de Alerta - Straemer App"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo errar el correo exitosamente. Error: {ex.Message}");
            }
        }
    }
}
