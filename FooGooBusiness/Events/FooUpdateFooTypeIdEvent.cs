using System;

namespace FooGooBusiness.Events
{
    public class FooUpdateFooTypeIdEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.UpdateFooFooTypeId;

        public Guid Id { get; set; }

        public Guid FooTypeId { get; set; }
    }
}