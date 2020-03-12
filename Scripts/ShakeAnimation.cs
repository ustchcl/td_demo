
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ShakeAnimation : MonoBehaviour {
    Monster enemy;

    void Start() {
        enemy = GetComponent<Monster>();
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {            
            float x = Random.Range(-1f, 1f) * magnitude + orignalPosition.x;
            float z = Random.Range(-1f, 1f) * magnitude + orignalPosition.z;

            transform.position = new Vector3(x, orignalPosition.y, z);
            elapsed += Time.deltaTime;
            yield return 0;
            orignalPosition.x += enemy.spdDirection.x * enemy.spd;
            orignalPosition.z += enemy.spdDirection.z * enemy.spd;
        }
        transform.position = orignalPosition;
    }
}