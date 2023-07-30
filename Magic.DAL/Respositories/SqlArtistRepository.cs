using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCards.DAL.Respositories
{
    public class SqlArtistRepository : IArtistRepository
    {
        private readonly mtgContext _db;

        public SqlArtistRepository(mtgContext mtgContext)
        {
            _db = mtgContext;
        }

        public IQueryable<Artist> GetArtists()
        {
            return _db.Artists
                        .Select(a => new Artist
                        {
                            Id = a.Id,
                            FullName = a.FullName,
                            Cards = _db.Cards.Where(c => c.ArtistId == a.Id).ToList(),
                        });
        }

        public IQueryable<Artist> GetArtists(int limit)
        {
            return _db.Artists
                        .Take(limit)
                        .Select(a => new Artist
                        {
                            Id = a.Id,
                            FullName = a.FullName,
                            Cards = _db.Cards.Where(c => c.ArtistId == a.Id).ToList(),
                        });
        }

        public Artist GetArtistById(int id)
        {
            return _db.Artists
                        .Select(a => new Artist
                        {
                            Id = a.Id,
                            FullName = a.FullName,
                            Cards = _db.Cards.Where(c => c.Artist.Id == id).ToList()
                        })
                        .Where(a => a.Id == id)
                        .First();
        }

        public IQueryable<Card> GetCardsByArtistId(long artistId)
        {
            return _db.Cards.Where(c => c.ArtistId == artistId);
        }
    }
}
