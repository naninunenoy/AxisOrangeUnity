using System;

namespace AxisOrange {
    public interface ISensor : IDisposable {
        int SensorId { get; }
        void Open();
        void Close();
    }
}
