using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Sky;
using UnityEngine.UI;


namespace Gameplay.Planet
{
    public class PlantReceiver : GenericReceiver
    {
        [SerializeField] ReceiverConfig config;
        ReceiverConfig.PhaseConfig nextPhase;
        public override void NotifyController(BodyType notifyEvent, object[] parameters)
        {
            base.NotifyController(notifyEvent, parameters);
            actualPhase = 0;
            deltaPhase = actualPhase;
        }

        public new void Update()
        {
            base.Update();
            actualPhase = config.ValidateEnergy(sunValue, moonValue, spr);
        }

        protected override void HandleMoonEnergy(float distantio)
        {
            if(moonValue <= nextPhase.moonEnergyReq + 4)
                base.HandleMoonEnergy(distantio);

            moonVal.fillAmount = Mathf.InverseLerp(lastPhaseMoon, phaseMoon, moonValue);
            sunVal.fillAmount = Mathf.InverseLerp(lastPhaseSun, phaseSun, sunValue);
            if (sunValue > 0)
                sunValue -= 0.25f * Time.deltaTime;
        }

        protected override void HandleSunEnergy(float distantio)
        {
            if(sunValue <= nextPhase.sunEnergyReq + 4)
                base.HandleSunEnergy(distantio);

            sunVal.fillAmount = Mathf.InverseLerp(lastPhaseSun, phaseSun, sunValue);
            moonVal.fillAmount = Mathf.InverseLerp(lastPhaseMoon, phaseMoon, moonValue);
            if (moonValue > 0)
                moonValue -= 0.25f * Time.deltaTime;
        }

        public override void HandlePhaseChange()
        {
            nextPhase = config.GetPhase(actualPhase + 1);
            ReceiverConfig.PhaseConfig currPhase = config.GetPhase(actualPhase);
            lastPhaseMoon = currPhase.moonEnergyReq;
            lastPhaseSun = currPhase.sunEnergyReq;
            phaseSun = nextPhase.sunEnergyReq;
            phaseMoon = nextPhase.moonEnergyReq;

            //Update fill ammouts
            sunVal.fillAmount = Mathf.InverseLerp(lastPhaseSun, phaseSun, sunValue);
            moonVal.fillAmount = Mathf.InverseLerp(lastPhaseMoon, phaseMoon, moonValue);

        }
    }
}