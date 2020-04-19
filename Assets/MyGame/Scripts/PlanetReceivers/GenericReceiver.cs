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
        [SerializeField] public float moonThreshold, sunThreshold;
        [HideInInspector] public int actualPhase, deltaPhase;

        [SerializeField] protected ReceiverConfig config;
        protected ReceiverConfig.PhaseConfig nextPhase;

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
        /// <summary>
        /// Void called when the actual phase from config has changed
        /// </summary>
        public abstract void HandlePhaseChange();

        /// <summary>
        /// Called when sun energy is bigger than the actual phase + sun threshold
        /// </summary>
        public abstract void HandleSunOverload();


        /// <summary>
        /// Called when moon energy is bigger than the actual phase + moon threshold
        /// </summary>
        public abstract void HandleMoonOverload();

        /// <summary>
        /// Called when sun energy is smaller than the actual phase min sun val - sun threshold
        /// </summary>
        public abstract void HandleSunUnderload();

        /// <summary>
        /// Called when moon energy is smaller than the actual phase min moon val - moon threshold
        /// </summary>
        public abstract void HandleMoonUnderload();

        protected virtual void HandleSunEnergy(float distantio)
        {

            MoonEnergyLoss();
            UpdateGraphics();
            sunValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio) * Time.deltaTime;
            sunValue = Mathf.Clamp(sunValue, minEnergyValueSun, nextPhase.sunEnergyReq + sunThreshold);
            if (sunValue >= nextPhase.sunEnergyReq)
                HandleSunOverload();
            
        }

        

        protected virtual void HandleMoonEnergy(float distantio)
        {
            SunEnergyLoss();
            UpdateGraphics();
            moonValue += Mathf.InverseLerp(treshHoldDistance, 2, distantio) * Time.deltaTime;
            moonValue = Mathf.Clamp(moonValue, minEnergyValueMoon, nextPhase.moonEnergyReq + moonThreshold);
            if (moonValue >= nextPhase.moonEnergyReq)
                HandleMoonOverload();

        }

        protected virtual void SunEnergyLoss() {
            sunValue = Mathf.Clamp(sunValue - 0.15f * Time.deltaTime, minEnergyValueSun - sunThreshold, phaseSun + sunThreshold);
            if (sunValue <= minEnergyValueSun)
                HandleSunUnderload();
                
        }

        protected virtual void MoonEnergyLoss()
        {
            moonValue = Mathf.Clamp(moonValue - 0.15f * Time.deltaTime, minEnergyValueMoon - moonThreshold, phaseMoon + moonThreshold);
            if (moonValue <= minEnergyValueMoon)
                HandleMoonUnderload();
        }

        protected virtual void UpdateGraphics()
        {
            sunVal.fillAmount = Mathf.InverseLerp(lastPhaseSun, phaseSun, sunValue);
            moonVal.fillAmount = Mathf.InverseLerp(lastPhaseMoon, phaseMoon, moonValue);
        }
    }
}