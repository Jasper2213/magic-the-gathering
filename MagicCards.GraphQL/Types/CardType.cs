namespace MagicCards.GraphQL.Types
{
    public class CardType : ObjectGraphType<Card>
    {
        public CardType()
        {
            Field(c => c.Id, type: typeof(IdGraphType))
                .Description("Id of the card")
                .Name("Id");

            Field(c => c.Name, type: typeof(StringGraphType))
                .Description("Name of the card")
                .Name("Name");

            Field(c => c.ManaCost, type: typeof(StringGraphType))
                .Description("Mana cost of the card")
                .Name("ManaCost");

            Field(c => c.Type, type: typeof(StringGraphType))
                .Description("Type of the card")
                .Name("Type");

            Field(c => c.RarityCode, type: typeof(StringGraphType))
                .Description("Rarity code of the card")
                .Name("RarityCode");

            Field(c => c.SetCode, type: typeof(StringGraphType))
                .Description("Set code of the card")
                .Name("SetCode");

            Field(c => c.Text, type: typeof(StringGraphType))
                .Description("Text of the card")
                .Name("Text");

            Field(c => c.Flavor, type: typeof(StringGraphType))
                .Description("Flavor text of the card")
                .Name("Flavor");

            Field(c => c.Power, type: typeof(StringGraphType))
                .Description("Power of the card")
                .Name("Power");

            Field(c => c.Toughness, type: typeof(StringGraphType))
                .Description("Toughness of the card")
                .Name("Toughness");

            Field(c => c.Layout, type: typeof(StringGraphType))
                .Description("Layout of the card")
                .Name("Layout");

            Field(c => c.OriginalImageUrl, type: typeof(StringGraphType))
                .Description("Original image URL of the card")
                .Name("OriginalImageUrl");

            Field(c => c.Image, type: typeof(StringGraphType))
                .Description("Image URL of the card")
                .Name("Image");

            Field<ArtistType>(
                "Artist",
                resolve: context => context.Source.Artist,
                description: "Artist of the card"
            );
        }
    }
}
