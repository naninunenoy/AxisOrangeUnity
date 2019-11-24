using System;
using UnityEngine;
using UniRx;

namespace AxisOrangeExample {
    public interface ISensorPresenter {
        IObservable<Unit> AddButtonClickObservable { get; }
        IObservable<Unit> CloseButtonClickObservable { get; }
        IObservable<Unit> InstallGyroOffsetButtonClickObservable { get; }
        string GetIdInputText();
        void SetComPortText(string text);
        void SetButtonAAppeared(bool appeared);
        void SetButtonBAppeared(bool appeared);
        void SetM5StickCRotation(Quaternion rotation);
        void OpenSensorView();
        void CloseSensorView();
    }
}
