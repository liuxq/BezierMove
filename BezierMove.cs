using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMove : MonoBehaviour {
    public float length = 10;
    public float offset = 4;
    public float totalTime = 5;
    public int segCount = 4;
    
    private Transform trans = null;
    private float curTime = 0;

    private List<Vector3> points = new List<Vector3>();

    private Vector3 start;

    void OnEnable()
    {
        points.Clear();
        curTime = totalTime;
        trans = this.transform;

        start = trans.position;
        Vector3 end = start + trans.forward * length;
        Vector3 dir = (end - start).normalized;
        float step = length / segCount;

        Vector3 right = trans.right;

        points.Add(start);

        for (int i = 1; i <= segCount; i++ )
        {
            points.Add(start + dir * (float)(step * ( i - 0.5)) + right * offset);
            offset = -offset;
            points.Add(start + dir * step * i);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(curTime > 0)
        {
            float t = (1 - curTime / totalTime) * segCount;
            int index = (int)Mathf.Floor(t);
            t -= index;

            Vector3 p1 = points[index * 2];
            Vector3 p2 = points[index * 2 + 1];
            Vector3 p3 = points[index * 2 + 2];

            Vector3 p = (1 - t) * (1 - t) * p1 + 2 * t * (1 - t) * p2 + t * t * p3;
            trans.position = p;
            curTime -= Time.deltaTime;

        }
	}

    void OnDrawGizmos()
    {
        for(int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawSphere(points[i], 0.1f);
        }
        
    }

    void OnDisable()
    {
        trans.position = start;
    }
}
