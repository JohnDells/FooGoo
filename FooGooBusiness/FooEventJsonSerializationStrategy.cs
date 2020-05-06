using Newtonsoft.Json;
using System.IO;

namespace FooGooBusiness
{
    public class FooEventJsonSerializationStrategy : IFooEventSerializationStrategy
    {
        private readonly JsonSerializer subEventSerializer;

        public FooEventJsonSerializationStrategy()
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new FooGooEventConverter());
            subEventSerializer = serializer;
        }

        public string Serialize(IFooGooEvent subEvent)
        {
            return JsonConvert.SerializeObject(subEvent);
        }

        public IFooGooEvent Deserialize(string value)
        {
            var stringReader = new StringReader(value);
            var reader = new JsonTextReader(stringReader);
            var result = subEventSerializer.Deserialize<IFooGooEvent>(reader);
            return result;
        }
    }
}