using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisOrange {
    public enum ButtonState { Release, Push }
    public readonly struct AxisOrangeButton {
        public readonly uint timestamp;
        public readonly ButtonState buttonA;
        public readonly ButtonState buttonB;

        public AxisOrangeButton(uint timestamp, byte bits) {
            this.timestamp = timestamp;
            buttonA = ((bits & 0x01) == 0) ? ButtonState.Release : ButtonState.Push;
            buttonB = ((bits & 0x02) == 0) ? ButtonState.Release : ButtonState.Push;
        }
    }
}
