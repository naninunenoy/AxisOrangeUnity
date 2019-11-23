using System;
using System.IO.Ports;
using UnityEngine;

namespace AxisOrange {
    internal class SerialReader {
        public bool TryReadSerialHeader(SerialPort serial, ref SerialHeader header) {
            if (!serial.IsNotNullAndOpened()) {
                return false;
            }
            var buf = new byte[SerialHeaderDef.HeaderLength];
            if (serial.Read(buf, 0, SerialHeaderDef.HeaderLength) != SerialHeaderDef.HeaderLength) {
                return false;
            }
            header = new SerialHeader(buf);
            return true;
        }

        public bool TryReadImuData(SerialPort serial, int dataLength, ref AxisOrangeData data) {
            if (!serial.IsNotNullAndOpened()) {
                return false;
            }
            var buf = new byte[dataLength];
            if (serial.Read(buf, 0, dataLength) != dataLength) {
                return false;
            }
            var t = buf.ToUInt(0);
            var acc = buf.ToVector3(4);
            var gyro = buf.ToVector3(16);
            var quat = buf.ToQuaternion(28);
            data = new AxisOrangeData(t, acc, gyro, quat);
            return true;
        }

        public bool TryReadButtonData(SerialPort serial, int dataLength, ref AxisOrangeButton data) {
            if (!serial.IsNotNullAndOpened()) {
                return false;
            }
            var buf = new byte[dataLength];
            if (serial.Read(buf, 0, dataLength) != dataLength) {
                return false;
            }
            var t = buf.ToUInt(0);
            data = new AxisOrangeButton(t, buf[4]);
            return true;
        }
    }

    internal static class ByteArrayConvertEx {
        public static ushort ToUShort(this byte[] byteArray, int offset) {
            if (byteArray == null) {
                return 0;
            }
            return BitConverter.ToUInt16(byteArray, offset);
        }
        public static uint ToUInt(this byte[] byteArray, int offset) {
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
