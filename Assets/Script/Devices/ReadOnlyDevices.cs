using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Script.Devices
{
    public class ReadOnlyDevices : MonoBehaviour
    {
        private TextMeshProUGUI text;

        private float _waitTime = 1;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            while (true)
            {
                Connect(DeviceIndex.IndoorTemp);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.OutdoorTemp);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.Power);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.FireAlarm);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.DoorAlarm);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.WaterLeakage);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.Stove);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.Window);
                yield return new WaitForSeconds(_waitTime);
                
                Connect(DeviceIndex.LightSensor);
                yield return new WaitForSeconds(_waitTime);
                

            }
        }


        private void Connect(DeviceIndex deviceIndex)
        {
            //var temp = ServerConnection.Get(deviceIndex);
            //var read = int.Parse(temp);
            text.SetText($"{deviceIndex.GetText()}: {0}");
        }
        
        
        

        
    }
}
