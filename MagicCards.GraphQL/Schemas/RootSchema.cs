namespace MagicCards.GraphQL.Schemas
{
    public class RootSchema : Schema
    {
        public RootSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
        }
    }
}
