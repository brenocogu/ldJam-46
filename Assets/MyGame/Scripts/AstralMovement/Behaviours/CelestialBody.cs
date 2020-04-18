using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.vrglass.eventcontroller;

namespace Gameplay.Sky
{

    public enum BodyType
    {
        SUN,
        MOON
    }

    public class CelestialBody : MonoBehaviour
    {
        public BodyType cBody;
        [SerializeField] CelestialBody opposite;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            App.Notify<BodyType>(cBody, new object[] { this, opposite });
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            App.Notify<BodyType>(cBody, new object[] { this, opposite });
        }
    }
}