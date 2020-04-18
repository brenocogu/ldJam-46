using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Sky
{
    public class AstralController : MonoBehaviour
    {
        float startSpeed, deltaKey;
        [SerializeField] float speed, damping;
        // Update is called once per frame
        private void Start()
        {
            startSpeed = speed;
        }
        void Update()
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                if (speed > 0)
                {
                    transform.Rotate(Vector3.forward * speed * deltaKey);
                    speed -= damping;
                }
            }

            else
            {
                deltaKey = Input.GetAxisRaw("Horizontal");
                transform.Rotate(Vector3.forward * speed * deltaKey);
                speed = startSpeed;
            }
        }
    }
}