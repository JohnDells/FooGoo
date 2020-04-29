using FooGooEf;
using Microsoft.EntityFrameworkCore;
using System;

namespace FooGooBusiness.Test
{
    public static class FooGooTestHelpers
    {
        public static FooGooContext GetDummyContext()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<FooGooContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var context = new FooGooContext(options);
            return context;
        }
    }
}