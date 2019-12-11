using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Script.Devices
{
    public class ReadOnlyDevices : MonoBehaviour
    {
        private string[] _status = new string[9]; 
        private TextMeshProUGUI text;

        private float _waitTime = 0.33f;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            while (true)
            {
                Connect(DeviceIndex.IndoorTemp, 0);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.OutdoorTemp, 1);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.Power, 2);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.FireAlarm, 3);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.DoorAlarm, 4);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.WaterLeakage, 5);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.Stove, 6);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.Window, 7);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.LightSensor, 8);
                yield return new WaitForSeconds(_waitTime);
                

            }
        }


        private void Connect(DeviceIndex deviceIndex, int arrayIndex)
        {
            //var temp = ServerConnection.Get(deviceIndex);
            //var read = int.Parse(temp);
            _status[arrayIndex] = $"{deviceIndex.GetText()}:{0}";
            
            text.SetText(string.Join("    ", _status));
        }
        
        
        

        
    }
}
