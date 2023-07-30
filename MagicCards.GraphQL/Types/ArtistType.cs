namespace MagicCards.GraphQL.Types
{
    public class ArtistType : ObjectGraphType<Artist>
    {
        public ArtistType(IArtistRepository artistRepository) 
        {
            Name = "Artist";

            Field(a => a.Id, type: typeof(IdGraphType))
                .Description("Id of the artist")
                .Name("Id");

            Field(a => a.FullName, type: typeof(StringGraphType))
                .Description("Full name of the artsit")
                .Name("FullName");

            Field<ListGraphType<CardType>>(
                "Cards",
                resolve: context =>
                {
                    Artist artist = context.Source;
                    IEnumerable<Card> cards = artistRepository.GetCardsByArtistId(artist.Id);
                    
                    return cards;
                }
            );
        }
    }
}
