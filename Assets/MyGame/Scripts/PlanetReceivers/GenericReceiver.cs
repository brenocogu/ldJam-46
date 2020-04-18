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
        protected SpriteRenderer spr;
        [HideInInspector] public int actualPhase;

        private void Start()
        {
            spr = GetComponent<SpriteRenderer>();
        }

        public override void NotifyController(BodyType notifyEvent, object[] parameters)
        {

            //This will require math aswell (to see if object is near body)
            //SUN UNDER 6
            //mOON UNDER 7
            Debug.Log(Vector2.Distance(transform.position, (parameters[0] as MonoBehaviour).transform.position));
            if (Vector2.Distance(transform.position, (parameters[0] as MonoBehaviour).transform.position) < 6.8f)
            {
                activeBody = parameters[0] as CelestialBody;
                treshHoldDistance = 6.8f;
            }
            else
            {
                activeBody = parameters[1] as CelestialBody;
                treshHoldDistance = 7.2f;
            }
        }

        protected void Update()
        {
            if (activeBody)
            {
                float distantio = Vector2.Distance(transform.position, activeBody.transform.position);
                if(activeBody.cBody == BodyType.SUN)
                {
                    sunValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio) * Time.deltaTime;
                    //decrease moon
                }
                else
                {
                    moonValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio) * Time.deltaTime;
                    //Decrease sun
                }
            }
        }
    }
}