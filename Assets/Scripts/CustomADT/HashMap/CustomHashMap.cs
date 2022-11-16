using System;
    public class CustomHashMap<TKey, TValue>
    {
        //An array of Linked Lists to allow chaining...
        //also known as buckets.
        private CustomLinkedList<HashEntry<TKey, TValue>>[] _tables;

        private const int InitialLength = 16;
        private const float LoadFactor = 0.75f;
        public int Count { get; private set; }
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public CustomHashMap()
        {
            _tables = new CustomLinkedList<HashEntry<TKey, TValue>>[InitialLength];
        }

        private bool CapacityReached()
        {
            return Count >= _tables.Length * LoadFactor;
        }
        public void Add(TKey key, TValue value)
        {
            if (CapacityReached())
            {
                ExpandAndRehash();
            }
            var newEntry = new HashEntry<TKey, TValue>(key, value);
            int hashIndex = HashFunction(key);
            var entryPoint = _tables[hashIndex];
            
            if (entryPoint == null) //If the space is free
            {
                _tables[hashIndex] = new CustomLinkedList<HashEntry<TKey, TValue>>(newEntry);
                Count++;
                return;
            }

            if (entryPoint.Head.Data.Key.Equals(key))
            {
                throw new Exception("This key already exists. Make it unique.");
            }
            entryPoint.InsertAtTail(newEntry);
            Count++;
        }
        //This expands the array and rehashes all the values.
        //Don't look at this. It's nasty lmao.
        private void ExpandAndRehash()
        {
            var newLength = _tables.Length * 2;
            var newTables = new CustomLinkedList<HashEntry<TKey, TValue>>[newLength];
            Count = 0;
            foreach (var linkedList in _tables)
            {
                if (linkedList == null) continue;
                var currentNode = linkedList.Head;
                while (currentNode != null)
                {
                    var hashIndex = HashFunction(currentNode.Data.Key, newLength); //This is the only difference when hashing.
                    var entryPoint = newTables[hashIndex];
                    if (entryPoint == null)
                    {
                        newTables[hashIndex] = new CustomLinkedList<HashEntry<TKey, TValue>>(currentNode.Data);
                        Count++;
                    }
                    else
                    {
                        entryPoint.InsertAtTail(currentNode.Data);
                        Count++;
                    }
                    currentNode = currentNode.Next;
                }
            }
            _tables = newTables;
            //Console.WriteLine($"Expanding and rehashing...{_tables.Length}, with {Count} elements.");

        }
        public TValue GetValue(TKey key)
        {
            int hashIndex = HashFunction(key);
            var currentEntry = _tables[hashIndex].Head;

            while (currentEntry != null)
            {
                if (currentEntry.Data.Key.Equals(key))
                {
                    return currentEntry.Data.Value;
                }
                currentEntry = currentEntry.Next;
            }
            throw new Exception("Key doesn't exist.");
        }
        //From: https://www.dotnetlovers.com/article/147/implementation-of-hashtable-and-analysis-on-running-time
        private int HashFunction(TKey key, int tableLength = 16)
        {
            int hashIndex = 7;
            char[] keyChars = key.ToString().ToCharArray();

            for (int i = 0; i < keyChars.Length; i++)
            {
                var asciiVal = keyChars[i] * i;
                hashIndex = hashIndex * 31 + asciiVal;
            }
            if (tableLength > _tables.Length) //If we're currently expanding and rehashing.
            {
                return Math.Abs(hashIndex % tableLength);
            }
            return Math.Abs(hashIndex % _tables.Length);
        }
    }