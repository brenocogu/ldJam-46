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
            public float energyRequired;
        }

        [SerializeField] List<PhaseConfig> phases;

        public int ValidateSunEnergy(float energy,SpriteRenderer sprito)
        {
            PhaseConfig actualPhase = phases.Where(x => energy >= x.energyRequired).OrderBy(x => Mathf.Abs(x.energyRequired - energy)).First();
            sprito.sprite = actualPhase.visualChange;
            return phases.IndexOf(actualPhase);
        }
    }
}