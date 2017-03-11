using System.Collections.Generic;

namespace Net
{
    public class URLVariables : Dictionary<string, string>
    {
        public const string MIN_PRICE = "min_price";
        public const string MAX_PRICE = "max_price";

        private int _counter = 0;

        public URLVariables(params string[] tags)
        {
            foreach (string tag in tags)
            {
                this.Add(tag);
            }
        }

        public void Add(string value)
        {
            this.Add("param"+_counter, value);
            _counter++;
        }

        public string URLString
        {
            get
            {
                string output = "?";
                foreach (string key in this.Keys)
                {
                    output += key + "=" + this[key] + "&";
                }
                return output;
            }
        }
    }
}