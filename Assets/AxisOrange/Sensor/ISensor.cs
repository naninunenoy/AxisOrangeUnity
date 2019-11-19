using System;

namespace AxisOrange {
    public interface ISensor : IDisposable {
        void Open();
        void Close();
    }
}
