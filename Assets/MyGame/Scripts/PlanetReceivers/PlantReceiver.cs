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
        }

        protected override void HandleSunEnergy(float distantio)
        {
            if(sunValue <= nextPhase.sunEnergyReq + 4)
                base.HandleSunEnergy(distantio);
        }

        public override void HandlePhaseChange()
        {
            if (actualPhase == 2)
                actualPhase = 0;

            nextPhase = config.GetPhase(actualPhase + 1);
            ReceiverConfig.PhaseConfig currPhase = config.GetPhase(actualPhase);
            lastPhaseMoon = currPhase.moonEnergyReq;
            lastPhaseSun = currPhase.sunEnergyReq;
            phaseSun = nextPhase.sunEnergyReq;
            phaseMoon = nextPhase.moonEnergyReq;

            UpdateGraphics();
        }
    }
}