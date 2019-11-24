using UnityEngine;

// thanks to https://gist.github.com/mao-test-h/4f5205bc22cdadaa1f01d9b286c48a56

namespace AxisOrange {
    public partial class MonoAxisOrangePlugin : MonoBehaviour {
        static MonoAxisOrangePlugin instance;
        public static MonoAxisOrangePlugin Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType(typeof(MonoAxisOrangePlugin)) as MonoAxisOrangePlugin;
                    if ((instance == null)) {
                        Debug.LogWarning(typeof(MonoAxisOrangePlugin) + " is nothing");
                    }
                }
                return instance;
            }
        }

        void Awake() {
            if (CheckInstance()) {
                DontDestroyOnLoad(gameObject);
            }
        }

        bool CheckInstance() {
            if (instance == null) {
                instance = this;
            } else {
                if (instance != this) {
                    // already exist destroy new one
                    Destroy(gameObject);
                    return false;
                }
            }
            return true;
        }
    }
}
