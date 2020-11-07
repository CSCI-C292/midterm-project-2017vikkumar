using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestLine : MonoBehaviour
{
    public GameObject[] dfs;


    //Helper Methods

    void DrawGizmos(bool bestLine)
    {

        if (dfs.Length > 1)
        {
            Vector3 prev = dfs[0].transform.position;
            for (int i = 1; i < dfs.Length; i++)
            {
                Vector3 next = dfs[i].transform.position;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(prev, dfs[0].transform.position);
        }
    }
}
