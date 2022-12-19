using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.GraphTypes;
using GraphQL;
using GraphQL.Types;
using System.Collections;
using System.Collections.Generic;

namespace Auto.Website.GraphQL.Queries
{
    public class OwnerQuery : ObjectGraphType
    {
        private IAutoDatabase _db;

        public OwnerQuery(IAutoDatabase db)
        {
            _db = db;
            Field<ListGraphType<OwnerGraphType>>("owners")
            .Resolve(GetOwner);
            Field<OwnerGraphType>("ownerNumberAd")
                .Argument<string>("numberAd")
                .Resolve(OwnerNumberAd);
        }

        private IEnumerable<Owner> GetOwner(IResolveFieldContext<object> context)
        {
            return _db.ListOwners();
        }
        
        private Owner OwnerNumberAd(IResolveFieldContext<object> context)
        {
            var numberAd = context.GetArgument<string>("numberAd");
            return _db.FindOwner(numberAd);
        }
    }
}
