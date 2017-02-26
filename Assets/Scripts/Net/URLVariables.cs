namespace Net
{
    public class URLVariables
    {
        string[] Params;
        //string[] Names;

        public URLVariables(params string[] _params)
        {
            Params = _params;
        }

        public string getString
        {
            get
            {
                string output = "?";
                    if(Params != null && Params.Length > 0)
                    {
//                        if (Names == null)
//                        {
                            for (int i = 0; i < Params.Length; i++)
                            {
                                output += "param" + i + "=" + Params[i] + "&";
                            }
//                        }
//                        else
//                        {
//                            for (int i = 0; i < Params.Length; i++)
//                            {
//                                output += Names[i] + "=" + Params[i] + "&";
//                            }
//                        }
                    }

                return output;
            }
        }
    }
}