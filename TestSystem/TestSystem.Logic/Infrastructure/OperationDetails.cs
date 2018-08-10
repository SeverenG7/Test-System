
namespace TestSystem.Logic.Infrastructure
{
    public class OperationDetails
    {
        public bool Succedeed { get; }
        public string Message { get; }
        public string Property { get; }
        public dynamic Value { get; }
        public string Id { get; }

        public OperationDetails
            (bool succedeed, string message, string property, dynamic value, string id)
        {
            Succedeed = succedeed;
            Message = message;
            Property = property;
            Value = value;
            Id = id;
        }

        public OperationDetails(bool succedeed, string message, string property)
        {
            Succedeed = succedeed;
            Message = message;
            Property = property;
        }
    }
}
