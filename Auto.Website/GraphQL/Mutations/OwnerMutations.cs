using Auto.Data.Entities;
using Auto.Data;
using Auto.Website.GraphQL.GraphTypes;
using GraphQL.Types;
using GraphQL;
using GraphQL.Validation;

namespace Auto.Website.GraphQL.Mutations
{
    public class OwnerMutations : ObjectGraphType
    {
        private readonly IAutoDatabase _db;

        public OwnerMutations(IAutoDatabase db)
        {
            _db = db;

            Field<OwnerGraphType>("createOwner")
                .Argument<string>("numberAd")
                .Argument<string>("firstName")
                .Argument<string>("lastName")
                .Resolve(Create);
            Field<OwnerGraphType>("updateOwner")
                .Argument<string>("numberAd")
                .Argument<string>("newFirstName")
                .Argument<string>("newLastName")
                .Resolve(Update);
        }

        private Owner Create(IResolveFieldContext<object> context)
        {
            var numberAd = context.GetArgument<string>("numberAd");
            var firstName = context.GetArgument<string>("firstName");
            var lastName = context.GetArgument<string>("lastName");

            var newOwner = new Owner
            {
                NumberAd = numberAd,
                FirstName = firstName,
                LastName = lastName,
                VehicleCode = "B433PXT"
            };

            _db.CreateOwner(newOwner);

            return _db.FindOwner(numberAd);
        }

        private Owner Update(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<string>("numberAd");
            var owner = _db.FindOwner(id);
            owner.FirstName = context.GetArgument<string>("newFirstName");
            owner.LastName = context.GetArgument<string>("newLastName");
            _db.UpdateOwner(owner);
            return _db.FindOwner(id);
        }
    }
}
