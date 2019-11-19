using System;
using System.Threading.Tasks;
using System.IO.Ports;
using UnityEngine;

namespace AxisOrange {
    public class AxisOrangeSensor : INotifySensor {
        const int ImuDataId = 1;
        const int ButtonDataId = 2;

        const int BoudRate = 115200;
        readonly string portNo;
        readonly SerialPort serialDevice;
        readonly SerialReader serialReader;
        public event Action<AxisOrangeData> OnSensorDataUpdate = delegate { };
        public event Action<AxisOrangeButton> OnSensorButtonUpdate = delegate { };
        bool isListening = false;

        public AxisOrangeSensor(int portNo) {
            this.portNo = $"COM{portNo}";
            serialDevice = new SerialPort(this.portNo, BoudRate);
            serialReader = new SerialReader();
        }

        public void Dispose() {
            serialDevice.Dispose();
        }

        public void Open() {
            if (serialDevice == null) return;
            try {
                serialDevice.Open();
            } catch (Exception e) {
                Debug.LogError($"cannot open serial port {portNo} {e.Message}");
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
                SerialRecieveLoop(serialDevice);
            });
        }

        public void Unlisten() {
            isListening = false;
        }

        async void SerialRecieveLoop(SerialPort serial) {
            SerialHeader header = default;
            AxisOrangeData data = default;
            AxisOrangeButton button = default;
            while (serialDevice.IsNotNullAndOpened()) {
                // serial read
                if (serialReader.TryReadSerialHeader(serial, ref header)) {
                    switch (header.dataId) {
                    case ImuDataId:
                        if (serialReader.TryReadImuData(serial, header.dataLength, ref data)) {
                            OnSensorDataUpdate.Invoke(data);
                        }
                        break;
                    case ButtonDataId:
                        if (serialReader.TryReadButtonData(serial, header.dataLength, ref button)) {
                            OnSensorButtonUpdate.Invoke(button);
                        }
                        break;
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
    }

    internal static class SerialPortEx {
        public static bool IsNotNullAndOpened(this SerialPort serial) {
            return serial != null && serial.IsOpen;
        }
    }
}
