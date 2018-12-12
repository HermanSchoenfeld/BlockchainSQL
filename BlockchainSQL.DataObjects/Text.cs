namespace BlockchainSQL.DataObjects {
    public class Text {

        public virtual int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual string en { get; set; }

        public virtual string es { get; set; }

        public virtual string pt { get; set; }

        public virtual string zh { get; set; }

        public virtual string ar { get; set; }

        public virtual string ru { get; set; }

        public virtual string fr { get; set; }

        public virtual string mn { get; set; }

        public virtual bool System { get; set; }

        /// <summary>
        /// Not mapped
        /// </summary>
        public virtual string Value { get; set; }
    }
}
