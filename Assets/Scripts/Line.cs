 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;

public class Line : MonoBehaviour
{
    public GameObject gameObject1;          // Reference to the first GameObject
    public GameObject gameObject2;          // Reference to the second GameObject

    private LineRenderer line;                           // Line Renderer

    // // Use this for initialization
    // void Start () {
    //     // Add a Line Renderer to the GameObject
    //     line = this.gameObject.AddComponent<LineRenderer>();
    //     // Set the width of the Line Renderer
    //     line.SetWidth(0.05F, 0.05F);
    //     // Set the number of vertex fo the Line Renderer
    //     line.SetVertexCount(2);
    // }
    public void DrawLines (Image parent, Image child)
    {
        line = parent.gameObject.AddComponent<LineRenderer>();
        //parent.gameObject.GetComponent<LineRenderer>();
        // Add a Line Renderer to the GameObject
        // if(parent.gameObject.GetComponent<LineRenderer>()==null)
        //     line = parent.gameObject.AddComponent<LineRenderer>();
        // else
        //     line = parent.gameObject.GetComponent<LineRenderer>();

        // Set the width of the Line Renderer
        //line.SetWidth(0.05F, 0.05F);
        line.startWidth = 0.05F;
        line.endWidth = 0.05F;
        // Set the number of vertex fo the Line Renderer
        line.positionCount = 2;

        line.SetPosition(0, parent.transform.position);
        line.SetPosition(1, child.transform.position);
    }
}
