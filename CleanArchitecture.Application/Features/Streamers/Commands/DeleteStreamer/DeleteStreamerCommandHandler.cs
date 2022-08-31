using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    internal class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStreamerCommand> _logger;

        public DeleteStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, ILogger<DeleteStreamerCommand> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await _streamerRepository.GetByIdAsync(request.Id);

            if (streamerToDelete != null)
            {
                _logger.LogError($"{request.Id} streamer no existe en el sistema");

                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            await _streamerRepository.DeleteAsync(streamerToDelete);

            _logger.LogError($"{request.Id} streamer fue eliminado con éxito");

            return Unit.Value;
        }
    }
}
