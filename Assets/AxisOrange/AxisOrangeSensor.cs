using System;
using System.Threading.Tasks;
using System.IO.Ports;

namespace AxisOrange {
    public class AxisOrangeSensor : IAxisOrangeSensor {

        const int BoudRate = 115200;
        readonly string portNo;
        readonly SerialPort serialDevice;
        readonly SerialReader serialReader;
        public event Action<AxisOrangeRawData> OnSensorDataUpdate = delegate { };
        public event Action<AxisOrangeButton> OnSensorButtonUpdate = delegate { };
        bool isListening = false;

        public AxisOrangeSensor(int portNo) {
            this.portNo = $"COM{portNo}";
            serialDevice = new SerialPort(this.portNo, BoudRate);
            serialReader = new SerialReader();
        }

        public void Dispose() {
            isListening = false;
            serialDevice.Dispose();
        }

        public void Open() {
            try {
                serialDevice.Open();
            } catch (Exception e) {
                throw e;
            }
        }

        public void Close() {
            if (serialDevice.IsNotNullAndOpened()) {
                serialDevice.Close();
            }
        }

        public void Listen() {
            isListening = true;
            Task.Run(() => {
                SerialRecieveLoop();
            });
        }

        public void Unlisten() {
            isListening = false;
        }

        async void SerialRecieveLoop() {
            SerialHeader header = default;
            AxisOrangeRawData data = default;
            AxisOrangeButton button = default;
            while (serialDevice.IsNotNullAndOpened()) {
                // serial read
                if (serialReader.TryReadSerialHeader(serialDevice, ref header)) {
                    if (header.dataId == SerialHeaderDef.ImuDataId) {
                        if (serialReader.TryReadImuData(serialDevice, header.dataLength, ref data)) {
                            OnSensorDataUpdate.Invoke(data);
                        }
                    } else if (header.dataId == SerialHeaderDef.ButtonDataId) {
                        if (serialReader.TryReadButtonData(serialDevice, header.dataLength, ref button)) {
                            OnSensorButtonUpdate.Invoke(button);
                        }
                    } else {
                        // Do Nothing
                    }
                }
                // finish loop?
                if (isListening) {
                    await Task.Delay(1);
                } else {
                    break;
                }
            }
        }
        public void RequestInstallGyroOfset() {
            serialDevice.Write(RequestId.InstallGyroOffset.ToRequestSerialHeaderBytes(), 0, SerialHeaderDef.HeaderLength);
        }
    }

    internal static class SerialPortEx {
        public static bool IsNotNullAndOpened(this SerialPort serial) {
            return serial != null && serial.IsOpen;
        }
    }
}
