using System.Collections.Generic;

namespace Net
{
    public class URLVariables : Dictionary<string, string>
    {
//        public string[] Params;
//        public string[] Names;
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


//        public URLVariables(params string[] _params)
//        {
//            Params = _params;
//        }
//
//        public string GetString
//        {
//            get
//            {
//                string output = "?";
//                    if(Params != null && Params.Length > 0)
//                    {
//                        for (int i = 0; i < Params.Length; i++)
//                        {
//                            if (Names == null || Names.Length <= i)
//                            {
//                                output += "param" + i + "=" + Params[i] + "&";
//                            }
//                            else
//                            {
//                                output += Names[i] + "=" + Params[i] + "&";
//                            }
//                        }
//                    }
//
//                return output;
//            }
//        }
    }
}