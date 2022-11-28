#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class EditorObjectPainter : MonoBehaviour
{
    public GameObject PaintBrush;
    public bool Active = false;
    [Range(1, 40)]
    public int PaintZone = 1;
    [Range(1,100)]
    public int Density = 1;
    [Min(0.1f)]
    public float Size = 1;
    public  List<GameObject> PaintableObj = new List<GameObject>();
    public  List<int> PaintableObjTrans = new List<int>();
    [Range(0, 10)]
    public int ActivePaintObj = 0;
    [Header("Options")]
    public bool RandomRotation;
    public bool RandomScale;
    [Range(0.1f, 0.5f)]
    public float ScaleDifference = 0.02f;
    public bool SaveSpawnedObj;
    public bool DeleteSpawnedObj;

    private GameObject PaintBrushInstance;
    private static Ray R;
    private bool Painting = false;
    private List<GameObject> dell = new List<GameObject>();
    void Start()
    {

        SceneView.duringSceneGui += OnSceneGUI;
    }
    void HandleMouse()
    {
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.MouseUp:
                if (e.button == 0) Painting = false;
                break;
            case EventType.MouseDrag:
                if (e.button == 0) Painting = true;
                break;
        }
    }
    private void OnSceneGUI(SceneView sceneView) 
    {
        if(Active)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            HandleMouse();
            if (PaintBrushInstance == null)
            {
                PaintBrushInstance = GameObject.Instantiate(PaintBrush);
            }
            PaintBrushInstance.transform.localScale = new Vector3(PaintZone, 1, PaintZone);
            Vector3 mousePosition = Event.current.mousePosition;
            R = HandleUtility.GUIPointToWorldRay(mousePosition);
            RaycastHit hitdatta;
            if (Physics.Raycast(R, out hitdatta))
            {
                PaintBrushInstance.transform.position = hitdatta.point;
            }
            if (Painting)
            {
                if (ActivePaintObj <= PaintableObj.Count - 1)
                {
                    Paint(hitdatta);
                }
                else
                {
                    Debug.Log("Index outside of objects array");
                    ActivePaintObj--;
                }
            }
        }
        else
        {
            if (PaintBrushInstance != null)
            {
                ClearSpawnedObj();
                DestroyImmediate(PaintBrushInstance);
            }
        }
        if (SaveSpawnedObj == false && dell.Count > 0)
        {
            dell.Clear();
        }
        if (DeleteSpawnedObj)
        {
            foreach(GameObject c in dell)
            {
                DestroyImmediate(c);
            }
            DeleteSpawnedObj = false;
        }
    }
    void Paint(RaycastHit Hit)
    {
        Collider[] LocPaintedObj = Physics.OverlapSphere(Hit.point, PaintZone / 2);
        int PaintZoneObj = 0;
        foreach (Collider c in LocPaintedObj)
        {
            if(c.gameObject.GetComponent<TagList>() != null)
            {
                if (c.gameObject.GetComponent<TagList>().HasTag("Decoration") == true)
                {
                    PaintZoneObj++;
                }
            }
        }
        if (PaintZoneObj < Density)
        {
            Vector3 ObjLocation = RandomLocation(Hit);
            if (ObjLocation != new Vector3(-99, -99, -99))
            {
                GameObject BrushActiveObj = GameObject.Instantiate(PaintableObj[ActivePaintObj]);
                if (SaveSpawnedObj)
                {
                    dell.Add(BrushActiveObj);
                }
                BrushActiveObj.AddComponent<TagList>().AddTag("Decoration");
                float transition = PaintableObjTrans[ActivePaintObj];
                BrushActiveObj.GetComponent<TagList>().AddTag("Outside");
                BrushActiveObj.transform.position = ObjLocation;
                BrushActiveObj.transform.localScale = new Vector3(Size,Size,Size);
                if (RandomRotation)
                {
                    BrushActiveObj.transform.rotation = new Quaternion(0, Random.Range(-180, 180), 0, 100);
                }
                if (RandomScale)//muta asta mai sus
                {
                    float tempScaleVar = Size * Random.Range(Size * Size - ScaleDifference, Size * Size + ScaleDifference);
                    Vector3 RandScale = new Vector3(tempScaleVar, tempScaleVar, tempScaleVar);
                    BrushActiveObj.transform.localScale = RandScale;

                }
                if (BrushActiveObj.GetComponent<LODGroup>())
                {
                    DestroyImmediate(BrushActiveObj.GetComponent<LODGroup>());
                }
                LOD l = new LOD();
                l.renderers = new Renderer[1] { BrushActiveObj.GetComponent<MeshRenderer>() };
                l.screenRelativeTransitionHeight = BrushActiveObj.transform.localScale.x / transition;
                LOD[] r = new LOD[] { l };
                BrushActiveObj.AddComponent<LODGroup>().SetLODs(r);
            }
        }
    }
    Vector3 RandomLocation(RaycastHit Hit)
    {
        Vector3 GroundPosition = new Vector3(-99, -99, -99);
        Vector3 HitPos = Hit.point + new Vector3(Random.Range(-PaintZone * 0.3f, PaintZone * 0.3f), 0, Random.Range(-PaintZone * 0.3f, PaintZone * 0.3f));
        RaycastHit hitdatta;
        Ray r = new Ray(HitPos + new Vector3(0, 120, 0), Vector3.down);
        if (Physics.Raycast(r, out hitdatta,450))
        {
            if (hitdatta.transform.gameObject.CompareTag("Floor")) // inlocuieste cu taggList
            {
                GroundPosition = hitdatta.point;
            }
        }
        else
        {
            GroundPosition = new Vector3(-99, -99, -99);
        }
        return GroundPosition;
    }
    void ClearSpawnedObj()
    {
        foreach(GameObject g in dell)
        {
            DestroyImmediate(g);
        }
    }
}
#endif