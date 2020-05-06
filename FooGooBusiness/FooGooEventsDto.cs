using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FooGooBusiness
{
    public class FooGooEventsDto
    {
        [JsonProperty("SubEvents", ItemConverterType = typeof(FooGooEventConverter))]
        public List<IFooGooEvent> SubEvents { get; set; }

        public Guid? CorrelationId { get; set; }
    }
}