using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes
{
    public sealed class ManufacturerGraphType : ObjectGraphType<Manufacturer>
    {
        public ManufacturerGraphType()
        {
            Name = "manufacterer";
            Field(c => c.Name).Description("The name of the manufacturer, e.g Tesla, Volkswagen, Ford");
        }
    }
}
