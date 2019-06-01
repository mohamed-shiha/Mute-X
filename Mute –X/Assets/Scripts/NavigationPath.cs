using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPath : MonoBehaviour {

    public Transform[] Nodes;
    public int NodeCount { get { return Nodes.Length; } }


    public Vector2 GetFirstNode()
    {
        return Nodes[0].position;
    }

    public Vector2 GetNodePosition(int index)
    {
        if (index >= 0 && index < NodeCount)
            return Nodes[index].position;
        else
        return Vector2.zero;
    }

    // this is for debugging 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(Nodes!= null)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (i + 1 < Nodes.Length)
                {
                    Gizmos.DrawLine(Nodes[i].position, Nodes[i + 1].position);
                }
                else
                {
                    Gizmos.DrawLine(Nodes[i].position, Nodes[0].position);
                }
            }//end for
        }// end if not null
    }

}
