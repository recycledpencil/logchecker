using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_host
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "/var/log/secure";
            if (args.Length != 0)
            {
                filename = args[0];
            }

            
            System.IO.StreamReader sr = new System.IO.StreamReader(filename);
            Dictionary<string, int> host_list = new Dictionary<string, int>();
            Dictionary<string, int> user_list = new Dictionary<string, int>();

            string st;
            while ((st = sr.ReadLine()) != null)
            {
                if (st.IndexOf("rhost") > 0)
                {
                    string[] temp = st.Split(' ');
                    foreach(string s in temp)
                    {
                        if (s.IndexOf("ruser") > -1)
                        {
                            string[] split = s.Split('=');
                            // キーの存在チェック
                            if (!user_list.ContainsKey(split[1]))
                            {
                                // 存在しない場合
                                user_list[split[1]] = 1;
                            }
                            else
                            {
                                user_list[split[1]]++;
                            }
                        }
                        if (s.IndexOf("rhost") > -1)
                        {
                            string[] split = s.Split('=');
                            // キーの存在チェック
                            if (!host_list.ContainsKey(split[1]))
                            {
                                // 存在しない場合
                                host_list[split[1]] = 1;
                            }
                            else
                            {
                                host_list[split[1]]++;
                            }
                        }

                    }
                }
            }
            sr.Close();

            System.IO.StreamWriter sw = new System.IO.StreamWriter("host_list.txt");

            foreach (KeyValuePair<string, int> kvp in host_list)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
                sw.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
            }
            sw.Close();

            sw = new System.IO.StreamWriter("user_list.txt");
            foreach (KeyValuePair<string, int> kvp in user_list)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
                sw.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
            }
            sw.Close();

        }
    }
}
