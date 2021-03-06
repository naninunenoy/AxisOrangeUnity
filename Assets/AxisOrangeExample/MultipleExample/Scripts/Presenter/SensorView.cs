﻿using UnityEngine;
using UnityEngine.UI;

namespace AxisOrangeExample {
    public class SensorView : MonoBehaviour, ISensorView {
        [SerializeField] Transform emptyView = default;
        [SerializeField] InputField idInput = default;
        [SerializeField] Button addButton = default;

        [SerializeField] Transform contentView = default;
        [SerializeField] Button startButton = default;
        [SerializeField] Button closeButton = default;
        [SerializeField] Button installGyroOffsetButton = default;
        [SerializeField] Text comPortText = default;
        [SerializeField] Text startButtonText = default;
        [SerializeField] Image buttonAImage = default;
        [SerializeField] Image buttonBImage = default;
        [SerializeField] Transform m5StickC = default;

        public Transform EmptyView => emptyView;
        public InputField IdInput => idInput;
        public Button AddButton => addButton;
        public Button StartButton => startButton;
        public Transform ContentView => contentView;
        public Button CloseButton => closeButton;
        public Button InstallGyroOffsetButton => installGyroOffsetButton;
        public Text ComPortText => comPortText;
        public Text StartButtonText => startButtonText;
        public Image ButtonAImage => buttonAImage;
        public Image ButtonBImage => buttonBImage;
        public Transform M5StickC => m5StickC;
    }
}
