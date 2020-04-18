using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.vrglass.eventcontroller;
using Gameplay.Sky;

namespace Gameplay.Planet
{
    public class GenericReceiver : EventController<BodyType>
    {
        protected CelestialBody activeBody;
        public float sunValue, moonValue, treshHoldDistance;

        public override void NotifyController(BodyType notifyEvent, object[] parameters)
        {
            Debug.Log("AAAA");
            //This will require math aswell (to see if object is near body)
            //SUN UNDER 6
            //mOON UNDER 7
            if (Vector2.Distance(transform.position, (parameters[0] as MonoBehaviour).transform.position) < 6)
            {
                activeBody = parameters[0] as CelestialBody;
                treshHoldDistance = 6;
            }
            else
            {
                activeBody = parameters[1] as CelestialBody;
                treshHoldDistance = 7;
            }
        }

        private void Update()
        {
            if (activeBody)
            {
                float distantio = Vector2.Distance(transform.position, activeBody.transform.position);
                if(activeBody.cBody == BodyType.SUN)
                {
                    sunValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio);
                    //decrease moon
                }
                else
                {
                    moonValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio);
                    //Decrease sun
                }
            }
        }
    }
}