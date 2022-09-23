namespace Category.Shared.Messaging.Contracts.Attribute
{
    public class AttributeValue
    {
        public AttributeValue() { }

        private AttributeValue(string id, string value)
        {
            Id = id;
            Value = value;
        }

        public string Id { get; set; }
        public string Value { get; set; }

        public static AttributeValue CreateAttributeValue(string id, string value)
            => new AttributeValue(id, value);
    }
}
