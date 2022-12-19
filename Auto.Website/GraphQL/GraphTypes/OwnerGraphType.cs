using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes
{
    public class OwnerGraphType : ObjectGraphType<Owner>
    {
        public OwnerGraphType()
        {
            Name = "owner";
            Field(c => c.NumberAd);
            Field(c => c.FirstName);
            Field(c => c.LastName);
            Field(c => c.OwnerVehicle, nullable:false, typeof(VehicleGraphType));
        }
    }
}
