using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using UGB.Paysa.Queries.Container;
using System;

namespace UGB.Paysa.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}