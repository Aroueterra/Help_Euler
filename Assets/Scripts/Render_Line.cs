using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render_Line : MonoBehaviour {
    public GameObject Obj1;
    public GameObject Obj2;
    public GameObject Obj3;
    public GameObject Obj4;
    public GameObject Obj5;
    public GameObject Obj6;

    public LineRenderer[] line;   
    void Start () {
        // Add a Line Renderer to the GameObject
        //line = this.gameObject.AddComponent<LineRenderer>();
        // Set the width of the Line Renderer
        DrawLines(line[0], 2, Color.red);
        DrawLines(line[1], 2, Color.red);
        DrawLines(line[2], 2, Color.red);
        DrawLines(line[3], 2, Color.red);
        DrawLines(line[4], 2, Color.red);
        DrawLines(line[5], 2, Color.red);
        DrawLines(line[6], 2, Color.red);
        //DrawLines(line3, 2, Color.red);

    }

    // Update is called once per frame
    void Update () {
        if (Obj1 != null && Obj2 != null)
        {
            // Update position of the two vertex of the Line Renderer
            DrawFromTo(line[0], Obj1.transform.position, Obj3.transform.position);
            DrawFromTo(line[1], Obj1.transform.position, Obj5.transform.position);
            DrawFromTo(line[2], Obj1.transform.position, Obj6.transform.position);
            DrawFromTo(line[3], Obj1.transform.position, Obj4.transform.position);
            DrawFromTo(line[4], Obj4.transform.position, Obj6.transform.position);
            DrawFromTo(line[5], Obj3.transform.position, Obj5.transform.position);
            DrawFromTo(line[6], Obj3.transform.position, Obj2.transform.position);
            //line3.SetPosition(1, Obj6.transform.position);
        }
    }
    void DrawLines(LineRenderer line, int Vertex, Color color)
    {
        line.useWorldSpace = true;
        line.sortingLayerName = "High Layer";
        line.startWidth = 0.09F;
        line.endWidth = 0.09F;
        // Set the number of vertex fo the Line Renderer
        line.positionCount = 2;
        line.sortingOrder = 1;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.material.color = Color.red;
    }
    void DrawFromTo(LineRenderer line, Vector3 From, Vector3 To)
    {
        line.SetPosition(0, From);
        line.SetPosition(1, To);
    } 
}
