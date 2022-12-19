﻿using Auto.Data.Entities;
using GraphQL.Types;
using System.Runtime.ExceptionServices;

namespace Auto.Website.GraphQL.GraphTypes
{
    public class VehicleGraphType: ObjectGraphType<Vehicle>
    {
        public VehicleGraphType()
        {
            Name = "vehicle";
            Field(c => c.VehicleModel, nullable: false, type: typeof(ModelGraphType))
                .Description("The model of this particular vehicle");
            Field(c => c.Owners, nullable: true, typeof(OwnerGraphType));
            Field(c => c.Registration);
            Field(c => c.Color);
            Field(c => c.Year);
        }
    }
}
