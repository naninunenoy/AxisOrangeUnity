using System;
using UniRx;

namespace AxisOrange {
    public static class AxisOrangeRxExtension {
        public static IReadOnlyReactiveProperty<AxisOrangeData> ReactiveSensorData(this IAxisOrangeSensor sensor) {
            return Observable
                .FromEvent<AxisOrangeData>(h => sensor.OnSensorDataUpdate += h, h => sensor.OnSensorDataUpdate -= h)
                .ToReadOnlyReactiveProperty();

        }
        public static IReadOnlyReactiveProperty<AxisOrangeUnityData> ReactiveUnityData(this IAxisOrangeSensor sensor) {
            return sensor
                .ReactiveSensorData()
                .Select(x => new AxisOrangeUnityData(x))
                .ToReadOnlyReactiveProperty();
        }
        public static IReadOnlyReactiveProperty<AxisOrangeButton> ReactiveButton(this IAxisOrangeSensor sensor) {
            return Observable
                .FromEvent<AxisOrangeButton>(h => sensor.OnSensorButtonUpdate += h, h => sensor.OnSensorButtonUpdate -= h)
                .ToReadOnlyReactiveProperty();
        }
        public static IObservable<Unit> ButtonAPushTriggerObservable(this IAxisOrangeSensor sensor) {
            return sensor
                .ReactiveButton()
                .Pairwise()
                .Where(x => x.Previous.buttonA == ButtonState.Release && x.Current.buttonA == ButtonState.Push)
                .Select(_ => Unit.Default);
        }
        public static IObservable<Unit> ButtonAReleaseTriggerObservable(this IAxisOrangeSensor sensor) {
            return sensor
                .ReactiveButton()
                .Pairwise()
                .Where(x => x.Previous.buttonA == ButtonState.Push && x.Current.buttonA == ButtonState.Release)
                .Select(_ => Unit.Default);
        }
        public static IObservable<Unit> ButtonAPushTriggerObservable(this IObservable<AxisOrangeButton> button) {
            return button
                .Pairwise()
                .Where(x => x.Previous.buttonA == ButtonState.Release && x.Current.buttonA == ButtonState.Push)
                .Select(_ => Unit.Default);
        }
        public static IObservable<Unit> ButtonAReleaseTriggerObservable(this IObservable<AxisOrangeButton> button) {
            return button
                .Pairwise()
                .Where(x => x.Previous.buttonA == ButtonState.Push && x.Current.buttonA == ButtonState.Release)
                .Select(_ => Unit.Default);
        }
        public static IObservable<Unit> ButtonBPushTriggerObservable(this IAxisOrangeSensor sensor) {
            return sensor
                .ReactiveButton()
                .Pairwise()
                .Where(x => x.Previous.buttonB == ButtonState.Release && x.Current.buttonB == ButtonState.Push)
                .Select(_ => Unit.Default);
        }
        public static IObservable<Unit> ButtonBReleaseTriggerObservable(this IAxisOrangeSensor sensor) {
            return sensor
                .ReactiveButton()
                .Pairwise()
                .Where(x => x.Previous.buttonB == ButtonState.Push && x.Current.buttonB == ButtonState.Release)
                .Select(_ => Unit.Default);
        }
        public static IObservable<Unit> ButtonBPushTriggerObservable(this IObservable<AxisOrangeButton> button) {
            return button
                .Pairwise()
                .Where(x => x.Previous.buttonB == ButtonState.Release && x.Current.buttonB == ButtonState.Push)
                .Select(_ => Unit.Default);
        }
        public static IObservable<Unit> ButtonBReleaseTriggerObservable(this IObservable<AxisOrangeButton> button) {
            return button
                .Pairwise()
                .Where(x => x.Previous.buttonB == ButtonState.Push && x.Current.buttonB == ButtonState.Release)
                .Select(_ => Unit.Default);
        }
    }
}
