using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static int dotsCount = 100;
    public float interval = 0.05f;
    public Vector3[] dotsPositions = new Vector3[dotsCount];
    public Transform[] transforms = new Transform[dotsCount];
    public GameObject prefab;
    public float addHeight = 0;
    public float speed = 3f;
    public int indexOfDot = 50;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < dotsCount; i++)
        {
            dotsPositions[i] = new Vector3(0, 0, i * interval);
            transforms[i] = GameObject.Instantiate<GameObject>(prefab).transform;
            transforms[i].position = dotsPositions[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(addHeight <= 0) return;

        var delta = Time.deltaTime * speed;
        addHeight -= delta;

        dotsPositions[indexOfDot] += new Vector3(1, 0, 0) * delta;

        // cal force
        for(int i = indexOfDot + 1; i < dotsCount; i++)
        {
            var preDot = dotsPositions[i - 1];
            var dotPos = dotsPositions[i];

            var distance = Vector3.Distance(preDot, dotPos);
            if(distance <= interval) break;

            var currentPos = Vector3.Lerp(preDot, dotPos, interval / distance);
            dotsPositions[i] = currentPos;
        }

        for(int i = indexOfDot - 1; i >= 0; i--)
        {
            var preDot = dotsPositions[i + 1];
            var dotPos = dotsPositions[i];

            var distance = Vector3.Distance(preDot, dotPos);
            if(distance <= interval) break;

            var currentPos = Vector3.Lerp(preDot, dotPos, interval / distance);
            dotsPositions[i] = currentPos;
        }

        for(int i = 0; i < dotsCount; i++)
        {
            transforms[i].position = dotsPositions[i];
        }
    }
}
