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

        public int ValidateSunEnergy(float sunEnergy, float moonEnergy,SpriteRenderer sprito)
        {
            PhaseConfig actualPhase = phases.Where(x => (sunEnergy >= x.sunEnergyReq && moonEnergy >= x.moonEnergyReq))
                                        .OrderBy(x => Mathf.Abs((x.sunEnergyReq - sunEnergy) + (x.moonEnergyReq - moonEnergy))).First();
            sprito.sprite = actualPhase.visualChange;
            return phases.IndexOf(actualPhase);
        }
    }
}