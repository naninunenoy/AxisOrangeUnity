using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

namespace AxisOrange {
    public class AxisOrange : MonoBehaviour {
        const int ReadHeaderLength = 4;
        public event Action<AxisOrangeData> DataReceived = delegate { };
        public event Action<AxisOrangeButton> ButtonUpdated = delegate { };

        [SerializeField] int portNo = 7;
        [SerializeField] int boudRate = 115200;

        SerialPort axisOrangeSerial = default;
        readonly byte[] readHeaderBuffer = new byte[ReadHeaderLength];

        void Awake() {
            try {
                axisOrangeSerial = new SerialPort($"COM{portNo}", boudRate);
            } catch (Exception e) {
                Debug.Log(e.Message);
                return;
            }
            try {
                axisOrangeSerial?.Open();
            } catch (Exception e) {
                Debug.LogError($"cannot open serial port {portNo} {e.Message}");
                return;
            }
            Debug.Log($"open serial port {portNo}");
        }

        void Update() {
            if (axisOrangeSerial == null) {
                return;
            }
            // header
            if (axisOrangeSerial.Read(readHeaderBuffer, 0, ReadHeaderLength) != ReadHeaderLength) {
                return;
            }
            // data
            var type = readHeaderBuffer.ToUShort(0);
            var len = readHeaderBuffer.ToUShort(2);
            var dataBuffer = new byte[len];
            if (axisOrangeSerial.Read(dataBuffer, 0, len) != len) {
                return;
            }
            // extract
            switch (type) {
            case 1:
                InvokeImuData(dataBuffer);
                break;
            case 2:
                InvokeButtonData(dataBuffer);
                break;
            }
        }

        void InvokeImuData(byte[] data) {
            var t = data.ToUInt(0);
            var acc = data.ToVector3(4);
            var gyro = data.ToVector3(16);
            var quat = data.ToQuaternion(28);
            DataReceived.Invoke(new AxisOrangeData(t, acc, gyro, quat));
        }

        void InvokeButtonData(byte[] data) {
            var t = data.ToUInt(0);
            ButtonUpdated.Invoke(new AxisOrangeButton(t, data[4]));
        }

        void OnDestroy() {
            if (axisOrangeSerial != null && axisOrangeSerial.IsOpen == true) {
                axisOrangeSerial.Close();
                axisOrangeSerial.Dispose();
                Debug.Log($"close serial port {portNo}");
            }
        }
    }
}
