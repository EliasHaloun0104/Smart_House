namespace Script
{
    public class Device
    {
        private string deviceId, deviceName, deviceStatus;
        
        public Device() {}

        public Device(string deviceId, string deviceName, string deviceStatus)
        {
            this.deviceId = deviceId;
            this.deviceName = deviceName;
            this.deviceStatus = deviceStatus;
        }

        public string DeviceId
        {
            get => deviceId;
            set => deviceId = value;
        }

        public string DeviceName
        {
            get => deviceName;
            set => deviceName = value;
        }

        public string DeviceStatus
        {
            get => deviceStatus;
            set => deviceStatus = value;
        }

        public override string ToString()
        {
            return "Device Id: " + deviceId + "\n" +
                   "Device Name: " + deviceName + "\n" +
                   "Device Status " + deviceStatus + "\n";
        }
    }
}