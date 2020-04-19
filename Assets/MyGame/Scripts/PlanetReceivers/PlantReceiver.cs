using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Sky;
using UnityEngine.UI;


namespace Gameplay.Planet
{
    public class PlantReceiver : GenericReceiver
    {
        [SerializeField] List<int> waterLevels;
        int currentWaterLevel;
        public override void NotifyController(BodyType notifyEvent, object[] parameters)
        {
            base.NotifyController(notifyEvent, parameters);
            actualPhase = 0;
            deltaPhase = actualPhase;
        }

        public new void Update()
        {
            base.Update();
            if(currentWaterLevel == waterLevels[actualPhase])
                actualPhase = config.ValidateEnergy(sunValue, moonValue, spr);
        }

        public override void HandlePhaseChange()
        {
            currentWaterLevel = 0;
            if (actualPhase == 2)
                actualPhase = 0;

            nextPhase = config.GetPhase(actualPhase + 1);
            ReceiverConfig.PhaseConfig currPhase = config.GetPhase(actualPhase);
            lastPhaseMoon = currPhase.moonEnergyReq;
            lastPhaseSun = currPhase.sunEnergyReq;
            phaseSun = nextPhase.sunEnergyReq;
            phaseMoon = nextPhase.moonEnergyReq;
            minEnergyValueMoon = lastPhaseMoon;
            minEnergyValueSun = lastPhaseSun;

            UpdateGraphics();
        }

        public void IncrementWaterLevel()
        {
            currentWaterLevel++;
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