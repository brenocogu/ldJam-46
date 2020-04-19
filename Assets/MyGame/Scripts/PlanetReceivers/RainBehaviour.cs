using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Planet {
    public class RainBehaviour : MonoBehaviour
    {
        float zRot;
        [SerializeField] float speed;
        private void Start()
        {
            if (Random.Range(-1, 1) == 0)
                speed = -speed;
            zRot = 180;
        }
        void Update()
        {
            zRot = Mathf.Clamp(zRot - speed * Time.deltaTime, 0, 360);
            transform.eulerAngles = Vector3.forward * zRot;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Plant"))
            {
                StartCoroutine(DespawnRoutine(collision.GetComponent<PlantReceiver>()));
            }
        }

        IEnumerator DespawnRoutine(PlantReceiver planto)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 1));
            planto.IncrementWaterLevel();
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
            transform.eulerAngles = Vector3.forward * 180;
            gameObject.SetActive(false);
        }
    }
}