using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

namespace AxisOrange {
    public class AxisOrange : MonoBehaviour {
        const int ReadDataLength = 44;
        public event Action<AxisOrangeData> DataReceived = delegate { };

        [SerializeField] int portNo = 7;
        [SerializeField] int boudRate = 115200;

        SerialPort axisOrangeSerial = default;
        readonly byte[] readDataBuffer = new byte[ReadDataLength];

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
            var len = axisOrangeSerial.Read(readDataBuffer, 0, ReadDataLength);
            if (len == ReadDataLength) {
                var t = readDataBuffer.ToLong(0);
                var acc = readDataBuffer.ToVector3(4);
                var gyro = readDataBuffer.ToVector3(16);
                var quat = readDataBuffer.ToQuaternion(28);
                var data = new AxisOrangeData(t, acc, gyro, quat);
                DataReceived.Invoke(data);
            }
        }

        void OnDestroy() {
            if (axisOrangeSerial != null && axisOrangeSerial.IsOpen == true) {
                axisOrangeSerial.Close();
                axisOrangeSerial.Dispose();
                Debug.Log($"close serial port {portNo}");
            }
        }
    }

    public static class ByteArrayConvertEx {
        public static long ToLong(this byte[] byteArray, int offset) {
            if (byteArray == null) {
                return 0;
            }
            return BitConverter.ToUInt32(byteArray, offset);
        }
        public static Vector3 ToVector3(this byte[] byteArray, int offset) {
            if (byteArray == null) {
                return Vector3.zero;
            }
            var x = BitConverter.ToSingle(byteArray, offset);
            var y = BitConverter.ToSingle(byteArray, offset + 4);
            var z = BitConverter.ToSingle(byteArray, offset + 8);
            return new Vector3(x, y, z);
        }
        public static Quaternion ToQuaternion(this byte[] byteArray, int offset) {
            if (byteArray == null) {
                return Quaternion.identity;
            }
            var w = BitConverter.ToSingle(byteArray, offset);
            var x = BitConverter.ToSingle(byteArray, offset + 4);
            var y = BitConverter.ToSingle(byteArray, offset + 8);
            var z = BitConverter.ToSingle(byteArray, offset + 12);
            return new Quaternion(x, y, z, w).normalized;
        }
    }
}
