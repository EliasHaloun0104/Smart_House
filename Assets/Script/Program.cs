using System;

namespace Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            RestClient client = new RestClient();
            client.endPoint = "";
            
            string strResponse = client.makeRequest();
            Console.WriteLine(strResponse);
        }
    }
}
