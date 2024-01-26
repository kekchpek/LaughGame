namespace LaughGame.GameResources
{
    public readonly struct ResourceId
    {

        private readonly string _id;

        private ResourceId(string id)
        {
            _id = id;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(_id);
        }

        public override bool Equals(object obj)
        {
            return obj is ResourceId id && Equals(id);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public bool Equals(ResourceId other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (_id != null ? _id.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return this;
        }

        public static bool operator ==(ResourceId resourceId1, ResourceId resourceId2)
        {
            return resourceId1._id == resourceId2._id;
        }

        public static bool operator !=(ResourceId resourceId1, ResourceId resourceId2)
        {
            return !(resourceId1 == resourceId2);
        }

        public static implicit operator ResourceId(string s)
        {
            return new ResourceId(s);
        }

        public static implicit operator string(ResourceId id)
        {
            return id._id;
        }
    }
}