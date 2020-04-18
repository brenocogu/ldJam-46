using com.vrglass.eventcontroller;
using UnityEngine;
using UnityEngine.UI;

namespace com.vrglass.eventcontroller
{
    [RequireComponent(typeof(Button))]
    public class NotifyButton : MonoBehaviour
    {
        public NOTIFY_EVENTS eventToCall;
        public string message;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        public void Click()
        {
            App.Notify(eventToCall, new object[] { message });
        }
    }
}