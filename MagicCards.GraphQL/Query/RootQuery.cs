using CardType = MagicCards.GraphQL.Types.CardType;

namespace MagicCards.GraphQL.Query
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(ICardRepository cardRepository, IArtistRepository artistRepository)
        {
            Name = "Query";

            #region Cards
            Field<ListGraphType<CardType>>(
                "Cards",
                Description = "Get all cards",
                resolve: context =>
                {
                    return cardRepository
                                .GetCards()
                                .ToList();
                }
            );
            #endregion

            #region Artists
            Field<ListGraphType<ArtistType>>(
                "Artists",
                Description = "Get all artists",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>
                    {
                        Name = "limit",
                        Description = "Limit how many artists are returned",
                        DefaultValue = 0
                    }
                ),
                resolve: context =>
                {
                    int limit = context.GetArgument<int>("limit");

                    if (limit > 0) return artistRepository
                                                .GetArtists(limit)
                                                .ToList();
                    else return artistRepository
                                    .GetArtists()
                                    .ToList();
                }
            );

            Field<ArtistType>(
                "Artist",
                Description = "Get an artist by their id",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType>
                    {
                        Name = "id",
                        Description = "Id of the artist",
                        DefaultValue = 1
                    }
                ),
                resolve: context =>
                {
                    int id = context.GetArgument<int>("id");
                    return artistRepository
                                .GetArtistById(id);
                }
            );
            #endregion
        }
    }
}
