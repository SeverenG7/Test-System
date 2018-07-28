
namespace TestSystem.Logic.Infrastructure
{
    public class OperationDetails
    {
        public bool Succedeed { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
        public dynamic Value { get; private set; }
        public string Id { get; private set; }

        public OperationDetails
            (bool succedeed , string message , string property , dynamic value, string id)
        {
            Succedeed = succedeed;
            Message = message;
            Property = property;
            Value = value;
            Id = id;
        }
    }
}
