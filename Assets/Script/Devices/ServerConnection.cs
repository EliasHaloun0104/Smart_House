using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Script.Devices
{
    public static class ServerConnection
    {
        public static string Get(DeviceIndex deviceIndex)
        {
            var client = new RestClient {endPoint = $"http://10.0.0.4:8080/SmartHouseApi/devices/{(int)deviceIndex}"};
            var strResponse = client.makeRequest();
            var json = JObject.Parse(strResponse);
            var lightStatus = json["deviceStatus"];
            return lightStatus.ToString();
        }
        

        public static void Put(DeviceIndex deviceIndex, int status)
        {
            //Set only the updated devices
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (deviceIndex)
            {
                //Update server by 0 = off or 100 = on
                case DeviceIndex.IndoorLamp:
                case DeviceIndex.OutdoorLamp:
                    status = status == 0 ? 100 : 0; 
                    //TODO Update on server
                     break;
                //Update server by between 0=off &  255= on (high speed)
                case DeviceIndex.Radiator:
                case DeviceIndex.Fan:
                    status = status + 51 <= 255 ? status + 51 : 0;
                    //TODO Update on server
                    
                    break;
                //Update On Local Machine 
                case DeviceIndex.Timer1:
                case DeviceIndex.Timer2:
                case DeviceIndex.Music:
                case DeviceIndex.Call112:
                case DeviceIndex.Call1177:
                case DeviceIndex.UnderContraction:
                    //Do nothing
                    break;
            }
        }
    }
}
