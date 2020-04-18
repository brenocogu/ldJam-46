using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.vrglass.eventcontroller;
using Gameplay.Sky;

namespace Gameplay.Planet
{
    public abstract class GenericReceiver : EventController<BodyType>
    {
        protected CelestialBody activeBody;
        protected float sunValue, moonValue;
        
        public override void NotifyController(BodyType notifyEvent, object[] parameters)
        {
            //This will require math aswell (to see if object is near body)
            activeBody = parameters[0] as CelestialBody;
        }

        private void Update()
        {
            if (activeBody)
            {
                //TODO see body limits to send events
            }
        }
    }
}