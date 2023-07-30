namespace MagicCards.WebAPI.Controllers.V11
{
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private const string _responseKey = "allCards";
        private const string _filterKey = "cardFilter";

        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CardsController(ICardRepository cardRepo, IMapper mapper, IMemoryCache memoryCache)
        {
            _cardRepo = cardRepo;
            _mapper = mapper;
            _cache = memoryCache;
        }

        [HttpGet()]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards([FromQuery] CardFilter filter) 
        {
            if (_cardRepo.GetCards() is IQueryable<Card> allCards)
            {
                // Check if the paged response is already in the cache
                var cachedResponse = _cache.Get(_responseKey) as PagedResponse<IEnumerable<CardReadDTO>>;

                if (cachedResponse == null ||
                    _cache.Get(_filterKey) is not CardFilter cachedFilter ||
                    !cachedFilter.Equals(filter))
                {
                    // Calculate the paged response and store it in the cache
                    cachedResponse = new PagedResponse<IEnumerable<CardReadDTO>>(
                        await allCards
                            .ToFilteredList(filter.Name, filter.Type, filter.Text, filter.ArtistFullName, filter.RarityName, filter.SetCode)
                            .ToPagedList(filter.PageNumber, filter.PageSize)
                            .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                            .ToListAsync(),
                        filter.PageNumber,
                        filter.PageSize)
                    {
                        TotalRecords = allCards.Count()
                    };
                    _cache.Set(_responseKey, cachedResponse);
                }

                return Ok(cachedResponse);
            }
            else
            {
                return NotFound(new Response<CardReadDTO>()
                {
                    Errors = new string[] { "404" },
                    Message = "No Cards found."
                });
            }
        }
    }
}

namespace MagicCards.WebAPI.Controllers.V15
{
    [ApiVersion("1.5")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private const string _responseKey = "allCards";
        private const string _filterKey = "cardFilter";

        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CardsController(ICardRepository cardRepo, IMapper mapper, IMemoryCache memoryCache)
        {
            _cardRepo = cardRepo;
            _mapper = mapper;
            _cache = memoryCache;
        }

        [HttpGet()]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards([FromQuery] CardFilter filter)
        {
            if (_cardRepo.GetCards() is IQueryable<Card> allCards)
            {
                // Check if the paged response is already in the cache
                var cachedResponse = _cache.Get(_responseKey) as PagedResponse<IEnumerable<CardReadDTO>>;

                if (cachedResponse == null ||
                    _cache.Get(_filterKey) is not CardFilter cachedFilter ||
                    !cachedFilter.Equals(filter))
                {
                    // Calculate the paged response and store it in the cache
                    cachedResponse = new PagedResponse<IEnumerable<CardReadDTO>>(
                        await allCards
                            .ToOrderedList(filter.OrderName)
                            .ToFilteredList(filter.Name, filter.Type, filter.Text, filter.ArtistFullName, filter.RarityName, filter.SetCode)
                            .ToPagedList(filter.PageNumber, filter.PageSize)
                            .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                            .ToListAsync(),
                        filter.PageNumber,
                        filter.PageSize)
                    {
                        TotalRecords = allCards.Count()
                    };
                    _cache.Set(_responseKey, cachedResponse);
                }

                return Ok(cachedResponse);
            }
            else
            {
                return NotFound(new Response<CardReadDTO>()
                {
                    Errors = new string[] { "404" },
                    Message = "No Cards found."
                });
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Response<CardReadDetailDTO>> GetCardById(int id)
        {
            return (_cardRepo.GetCardById(id) is Card foundCard)
                ? Ok(_mapper.Map<CardReadDetailDTO>(foundCard))
                : NotFound($"No card found with id {id}");
        }
    }
}