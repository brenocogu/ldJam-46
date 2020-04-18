using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.vrglass.eventcontroller;

namespace com.vrglass.eventcontroller
{
    //You need to specify the event type of the controller
    public class SampleController : EventController<NOTIFY_EVENTS>
    {
        public override void NotifyController(NOTIFY_EVENTS notifyEvent, object[] parameters)
        {
            switch (notifyEvent)
            {
                case NOTIFY_EVENTS.EVENT_SAMPLE:
                    print("Sample event fired message: " + parameters[0].ToString());
                    break;
            }
        }
    }
}