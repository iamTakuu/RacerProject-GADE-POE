    public class HashEntry <TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set;}
            
        public HashEntry(TKey key, TValue value)
        {
            Value = value;
            Key = key;
        }
    }