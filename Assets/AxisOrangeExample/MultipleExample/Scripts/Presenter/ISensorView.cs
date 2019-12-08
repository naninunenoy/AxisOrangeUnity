using UnityEngine;
using UnityEngine.UI;

namespace AxisOrangeExample {
    public interface ISensorView {
        Transform EmptyView { get; }
        InputField IdInput { get; }
        Button AddButton { get; }
        Button StartButton { get; }

        Transform ContentView { get; }
        Button CloseButton { get; }
        Button InstallGyroOffsetButton { get; }
        Text ComPortText { get; }
        Text StartButtonText { get; }
        Image ButtonAImage { get; }
        Image ButtonBImage { get; }
        Transform M5StickC { get; }
    }
}
