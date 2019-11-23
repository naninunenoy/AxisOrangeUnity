namespace AxisOrange {
    internal static class SerialHeaderDef {
        public const int HeaderLength = 4;
        public const int ImuDataId = 1;
        public const int ButtonDataId = 2;
    }
    internal readonly struct SerialHeader {
        public readonly int dataId;
        public readonly int dataLength;
        public SerialHeader(byte[] header) {
            dataId = header.ToUShort(0);
            dataLength = header.ToUShort(2);
        }
    }
}
