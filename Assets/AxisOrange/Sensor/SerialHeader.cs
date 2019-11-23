using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    internal readonly struct SerialHeader {
        public const int HeaderLength = 4;
        public readonly int dataId;
        public readonly int dataLength;
        public SerialHeader(byte[] header) {
            dataId = header.ToUShort(0);
            dataLength = header.ToUShort(2);
        }
    }
}
