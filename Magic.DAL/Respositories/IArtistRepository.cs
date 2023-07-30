using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCards.DAL.Respositories
{
    public interface IArtistRepository
    {
        IQueryable<Artist> GetArtists();
        IQueryable<Artist> GetArtists(int limit);
        Artist GetArtistById(int id);
        IQueryable<Card> GetCardsByArtistId(long artistId);
    }
}
