using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, IList<VideosViewModel>>
    {
        private readonly IVideoRepository? _videoRepository;
        private readonly IMapper? _mapper;

        public GetVideosListQueryHandler(IVideoRepository? videoRepository, IMapper? mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public IVideoRepository? Get_videoRepository()
        {
            return _videoRepository;
        }

        public async Task<IList<VideosViewModel>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _videoRepository.GetVideoByUserName(request.UserName);

            return _mapper.Map<List<VideosViewModel>>(videoList);
        }
    }
}
