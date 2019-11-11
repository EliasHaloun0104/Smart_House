using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Client;

namespace Script
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var devices = StatusAllDevices().Result;
            foreach (var d in devices)
            {
                Console.WriteLine(d);
            }

            string[] values = {"1", "indoorlamp", "off"};
            runJavaRestClient(values);

            var device = StatusDevice("1").Result;
            Console.WriteLine(device);
        }

        private static async Task<List<Device>> StatusAllDevices()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://10.2.3.40:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("SmartHouseApi/devices/");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Device>>();
                }

                return null;
            }
        }

        private static async Task<Device> StatusDevice(string deviceId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://10.2.3.40:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("SmartHouseApi/devices/" + deviceId);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Device>();
                }

                return null;
            }
        }

        private static void runJavaRestClient(string[] args)
        {
            //Create process
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            //strCommand is path and file name of command to run
            pProcess.StartInfo.FileName = "java";
            //strCommandParameters are parameters to pass to program
            pProcess.StartInfo.Arguments = "-jar /Users/nuno/temp/lib/target/javaRestClient-1.0-SNAPSHOT-jar-with-dependencies.jar " + args[0] + " " + args[1] + " " + args[2];
            pProcess.StartInfo.UseShellExecute = false;
            //Set output of program to be written to process output stream
            //Process.StartInfo.RedirectStandardOutput = true;
            //Start the process
            pProcess.Start();
            //Get program output
            //string strOutput = pProcess.StandardOutput.ReadToEnd();
            //Wait for process to finish
            pProcess.WaitForExit();
        }
    }
}