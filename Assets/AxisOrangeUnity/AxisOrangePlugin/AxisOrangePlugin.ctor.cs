using System.Collections.Generic;
using UniRx;

namespace AxisOrange {
    public partial class AxisOrangePlugin {
        static AxisOrangePlugin() {
            sensorDict = new Dictionary<int, IAxisOrangeSensor>();
            disposables = new Dictionary<int, CompositeDisposable>();
            factory = new AxisOrangeFactory();
        }
    }
}
