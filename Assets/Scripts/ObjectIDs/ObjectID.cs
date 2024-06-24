using System;

namespace ObjectIDs
{
    public class ObjectID
    {
        private Guid id;

        public ObjectID()
        {
            this.id = Guid.NewGuid();
        }

        public ObjectID(Guid id)
        {
            this.id = id;
        }

        public Guid GetID()
        {
            return this.id;
        }
    }
}