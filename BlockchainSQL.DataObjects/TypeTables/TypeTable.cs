namespace BlockchainSQL.DataObjects
{
    internal abstract class TypeTable {
        public virtual byte ID { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}
