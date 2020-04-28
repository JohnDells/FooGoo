﻿using System;

namespace FooGooBusiness.Events
{
    public class FooTypeCreateEvent : IFooGooEvent
    {
        public string Type => FooEventConstants.CreateFooType;

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}