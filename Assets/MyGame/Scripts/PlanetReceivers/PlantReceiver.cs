using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Sky;

namespace Gameplay.Planet
{
    public class PlantReceiver : GenericReceiver
    {
        [SerializeField] ReceiverConfig config;
        public override void NotifyController(BodyType notifyEvent, object[] parameters)
        {
            base.NotifyController(notifyEvent, parameters);

        }

        public new void Update()
        {
            base.Update();
            actualPhase = config.ValidateEnergy(sunValue, moonValue, spr);
        }

    }
}