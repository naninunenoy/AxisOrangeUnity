using UnityEngine;
using UnityEngine.UI;

namespace AxisOrangeExample {
    public class SensorView : MonoBehaviour, ISensorView {
        [SerializeField] Transform emptyView = default;
        [SerializeField] InputField idInput = default;
        [SerializeField] Button addButton = default;

        [SerializeField] Transform contentView = default;
        [SerializeField] Button closeButton = default;
        [SerializeField] Button installGyroOffsetButton = default;
        [SerializeField] Text comPortText = default;
        [SerializeField] Image buttonAImage = default;
        [SerializeField] Image buttonBImage = default;
        [SerializeField] Transform m5StickC = default;

        public Transform EmptyView => emptyView;
        public InputField IdInput => idInput;
        public Button AddButton => addButton;
        public Transform ContentView => contentView;
        public Button CloseButton => closeButton;
        public Button InstallGyroOffsetButton => installGyroOffsetButton;
        public Text ComPortText => comPortText;
        public Image ButtonAImage => buttonAImage;
        public Image ButtonBImage => buttonBImage;
        public Transform M5StickC => m5StickC;
    }
}
