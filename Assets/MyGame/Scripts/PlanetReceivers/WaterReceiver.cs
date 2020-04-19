using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Planet
{
    public class WaterReceiver : GenericReceiver
    {
        [SerializeField] ReceiverConfig config;
        ReceiverConfig.PhaseConfig nextPhase;
        public override void HandlePhaseChange()
        {
            if (actualPhase == 2)
            {
                sunValue = 0;
                moonValue = 0;
                actualPhase = 0;
            }

            nextPhase = config.GetPhase(actualPhase + 1);
            ReceiverConfig.PhaseConfig currPhase = config.GetPhase(actualPhase);
            lastPhaseMoon       = currPhase.moonEnergyReq;
            lastPhaseSun        = currPhase.sunEnergyReq;
            phaseSun            = nextPhase.sunEnergyReq;
            phaseMoon           = nextPhase.moonEnergyReq;
            minEnergyValueMoon  = lastPhaseMoon;
            minEnergyValueSun   = lastPhaseSun;
            UpdateGraphics();
            
        }

        public new void Update()
        {
            Debug.Log(actualPhase);
            base.Update();
            actualPhase = config.ValidateEnergy(sunValue, moonValue);
        }

        protected override void HandleSunEnergy(float distantio)
        {
            base.HandleSunEnergy(distantio);
        }
    }
}