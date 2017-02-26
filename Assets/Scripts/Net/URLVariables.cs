namespace Net
{
    public class URLVariables
    {
        public string[] Params;
        public string[] Names;

        public URLVariables(params string[] _params)
        {
            Params = _params;
        }

        public string GetString
        {
            get
            {
                string output = "?";
                    if(Params != null && Params.Length > 0)
                    {
                        for (int i = 0; i < Params.Length; i++)
                        {
                            if (Names == null || Names.Length <= i)
                            {
                                output += "param" + i + "=" + Params[i] + "&";
                            }
                            else
                            {
                                output += Names[i] + "=" + Params[i] + "&";
                            }
                        }
                    }

                return output;
            }
        }
    }
}