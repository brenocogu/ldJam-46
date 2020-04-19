using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Gameplay.Planet
{
    [CreateAssetMenu(menuName = "Gameplay/Receiver Configuration")]
    public class ReceiverConfig : ScriptableObject
    {
        [System.Serializable]
        public struct PhaseConfig
        {
            public Sprite visualChange;
            public float sunEnergyReq, moonEnergyReq;
        }

        [SerializeField] List<PhaseConfig> phases;

        public int ValidateEnergy(float sunEnergy, float moonEnergy,SpriteRenderer sprito = null)
        {
            if (sunEnergy < 0)
                sunEnergy = 0;
            if (moonEnergy < 0)
                moonEnergy = 0;
            PhaseConfig actualPhase = phases.Where(x => (sunEnergy >= x.sunEnergyReq && moonEnergy >= x.moonEnergyReq))
                                        .OrderBy(x => Mathf.Abs((x.sunEnergyReq - sunEnergy) + (x.moonEnergyReq - moonEnergy))).First();
            if(sprito)
                sprito.sprite = actualPhase.visualChange;

            return phases.IndexOf(actualPhase);
        }

        public PhaseConfig GetPhase(int indexo)
        {
            if (indexo >= 0 && indexo < phases.Count)
                return phases[indexo];
            else
                return phases.Last();
        }
    }
}