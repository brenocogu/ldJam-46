﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Planet
{
    public class WaterReceiver : GenericReceiver
    {
        [SerializeField] GameObject rainObj;
        public override void HandlePhaseChange()
        {
            if(actualPhase == 1)
                rainObj.SetActive(true);

            if (actualPhase == 2)
                actualPhase = 0;

            sunValue = 0;
            moonValue = 0;
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
            base.Update();
            actualPhase = config.ValidateEnergy(sunValue, moonValue);
        }

        public override void HandleMoonOverload()
        {
            
        }

        public override void HandleMoonUnderload()
        {
        }

        public override void HandleSunOverload()
        {

        }

        public override void HandleSunUnderload()
        {

        }
    }
}