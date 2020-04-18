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
        [SerializeField] BodyType cBody;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            App.Notify<BodyType>(cBody, new object[] { this });
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            App.Notify<BodyType>(cBody, new object[] { this });
        }
    }
}