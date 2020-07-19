using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCrosshair : MonoBehaviour
{
    private Texture2D texture;
    private GUIStyle lineStyle;
    private Color color;

    [SerializeField] private int height = 5;
    [SerializeField] private int width = 35;
    private Vector2 centrePoint;

    public int square = 20;
    public bool crosshair = true;

    [SerializeField] [Range(6, 80)] float offset;
   // public EquippedGunBehaviour egb;
    public EquippedGun equippedGun;
    void Start()
    {
        centrePoint = new Vector2(Screen.width / 2, Screen.height / 2);
        texture = new Texture2D(1, 1);
        color = Color.red;
        SetColor(texture, color);
        lineStyle = new GUIStyle();
        lineStyle.normal.background = texture;
    }

    // Update is called once per frame
    void Update()
    {
        float factor = equippedGun.currentAcc / equippedGun.currentGun.maxAccuracy;
        //Debug.Log("FACTOR   " + factor);
        offset = Mathf.Lerp(80f, 6f, factor);
        //   GUI.Box(new Rect)
    }

    private void OnGUI()
    {
        //offset = 3;

        if (crosshair)
        {
            GUI.Box(new Rect(centrePoint.x , centrePoint.y , 1, 1), GUIContent.none, lineStyle);

            GUI.Box(new Rect(centrePoint.x - width/2 + offset, centrePoint.y - height/2, width, height), GUIContent.none, lineStyle);  //Right
            GUI.Box(new Rect(centrePoint.x - width/2 - offset, centrePoint.y - height/2, width, height), GUIContent.none, lineStyle);  //left
            GUI.Box(new Rect(centrePoint.x - height/2, centrePoint.y - width / 2 + offset, height, width), GUIContent.none, lineStyle); // bottom
            GUI.Box(new Rect(centrePoint.x - height/2, centrePoint.y - width / 2 - offset, height, width), GUIContent.none, lineStyle); // up

            
            
            // GUI.Box(new Rect(centrePoint.x - 2 * width, centrePoint.y - height, width, height), GUIContent.none, lineStyle); // left
        }
    }

    void SetColor(Texture2D texture, Color color)
    {
        for (int i = 0; i < texture.height; i++)
            for (int j = 0; j < texture.width; j++)
                texture.SetPixel(i, j, color);
        texture.Apply();
    }
}
