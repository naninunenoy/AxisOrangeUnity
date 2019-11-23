namespace AxisOrange {
    public readonly struct AxisOrangeDataData {
        public readonly uint timestamp;
        public readonly float[] acc;
        public readonly float[] gyro;
        public readonly float[] quaternion;

        public AxisOrangeDataData(uint timestamp, float[] acc, float[] gyro, float[] quaternion) {
            this.timestamp = timestamp;
            this.acc = acc;
            this.gyro = gyro;
            this.quaternion = quaternion;
        }
    }
}
