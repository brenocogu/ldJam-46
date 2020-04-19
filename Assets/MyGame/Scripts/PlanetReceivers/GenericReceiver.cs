using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.vrglass.eventcontroller;
using Gameplay.Sky;

namespace Gameplay.Planet
{
    public abstract class GenericReceiver : EventController<BodyType>
    {
        protected CelestialBody activeBody;
        protected float sunValue, moonValue, treshHoldDistance;
        protected float phaseMoon, phaseSun;
        protected float lastPhaseMoon, lastPhaseSun;
        protected float minEnergyValueSun, minEnergyValueMoon;
        protected SpriteRenderer spr;
        [HideInInspector] public int actualPhase, deltaPhase;

        [SerializeField]protected Image sunVal, moonVal;
        private void Start()
        {
            minEnergyValueSun   = 0;
            minEnergyValueMoon  = 0;
            spr                 = GetComponent<SpriteRenderer>();
            HandlePhaseChange();
        }

        public override void NotifyController(BodyType notifyEvent, object[] parameters)
        {
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
                    HandleSunEnergy(distantio);
                }
                else
                {
                    HandleMoonEnergy(distantio);
                }
            }

            if(deltaPhase != actualPhase)
            {
                HandlePhaseChange();
                deltaPhase = actualPhase;
            }
        }

        public abstract void HandlePhaseChange();

        protected virtual void HandleSunEnergy(float distantio)
        {
            sunValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio) * Time.deltaTime;
            if (moonValue > minEnergyValueMoon)
                moonValue -= 0.15f * Time.deltaTime;
            UpdateGraphics();
        }

        protected virtual void HandleMoonEnergy(float distantio)
        {
            moonValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio) * Time.deltaTime;
            if (sunValue > minEnergyValueSun)
                sunValue -= 0.15f * Time.deltaTime;
            UpdateGraphics();
        }

        protected virtual void UpdateGraphics()
        {
            sunVal.fillAmount = Mathf.InverseLerp(lastPhaseSun, phaseSun, sunValue);
            moonVal.fillAmount = Mathf.InverseLerp(lastPhaseMoon, phaseMoon, moonValue);
        }
    }
}