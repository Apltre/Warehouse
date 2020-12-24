using System;

namespace Warehouse.Common
{
    public class LegacyKey
    {
        public int Key1 { get; set; }
        public int Key2 { get; set; }

        public LegacyKey() { }

        public LegacyKey(int Key1, int Key2)
        {
            this.Key1 = Key1;
            this.Key2 = Key2;
        }

        public static LegacyKey Parse(string key)
        {
            var keyParts = key.Split('-');

            if (keyParts.Length != 2)
            {
                throw new Exception("Wrong key format");
            }

            return new LegacyKey() {
                Key1 = Convert.ToInt32(keyParts[0]),
                Key2 = Convert.ToInt32(keyParts[1])
            };
        }

        public override string ToString()
        {
            return $"{this.Key1}-{this.Key2}";
        }
    }
}
