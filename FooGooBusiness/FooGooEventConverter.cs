using FooGooBusiness.Events;
using FooGooCommon;
using Newtonsoft.Json.Linq;
using System;

namespace FooGooBusiness
{
    public class FooGooEventConverter : JsonCreationConverter<IFooGooEvent>
    {
        protected override IFooGooEvent Create(Type objectType, JObject jObject)
        {
            var raw = jObject["EventType"] ?? jObject["EventType"] ?? jObject["eventType"] ?? jObject["eventtype"];
            if (raw == null) throw new ArgumentException("The foo event 'type' was not recognized.");

            var type = raw.Value<string>();
            switch (type)
            {
                case FooEventConstants.CreateFooType:
                    return new FooTypeCreateEvent();

                case FooEventConstants.UpdateFooTypeName:
                    return new FooTypeUpdateNameEvent();

                case FooEventConstants.DeleteFooType:
                    return new FooTypeDeleteEvent();

                case FooEventConstants.CreateFoo:
                    return new FooCreateEvent();

                case FooEventConstants.UpdateFooName:
                    return new FooUpdateNameEvent();

                case FooEventConstants.UpdateFooFooTypeId:
                    return new FooUpdateFooTypeIdEvent();

                case FooEventConstants.DeleteFoo:
                    return new FooDeleteEvent();

                case FooEventConstants.CreateBar:
                    return new BarCreateEvent();

                case FooEventConstants.UpdateBarName:
                    return new BarUpdateNameEvent();

                case FooEventConstants.DeleteBar:
                    return new BarDeleteEvent();

                default:
                    return null;
            }
        }
    }
}