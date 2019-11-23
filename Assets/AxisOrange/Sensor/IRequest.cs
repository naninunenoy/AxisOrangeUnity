using System;
using System.Linq;

namespace AxisOrange {
    public interface IRequest {
        void RequestInstallGyroOfset();
    }

    internal enum RequestId {
        InstallGyroOffset = 0x8001
    }

    internal static class RequestIdEx {
        public static byte[] ToRequestSerialHeaderBytes(this RequestId req) {
            ushort id = (ushort)req;
            ushort len = 0;
            return BitConverter.GetBytes(id).Concat(BitConverter.GetBytes(len)).ToArray();
        }
    }
}
