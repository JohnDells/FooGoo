namespace FooGooBusiness
{
    public interface IFooEventSerializationStrategy
    {
        string Serialize(IFooGooEvent item);

        IFooGooEvent Deserialize(string value);
    }
}