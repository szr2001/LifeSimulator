using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ObjectPaintBrush : MonoBehaviour
{
    public GameObject PaintBrushObj;
    public GameObject ScaleToggle;
    public GameObject RandRotToggle;
    public GameObject ScaleSlider;
    public GameObject DensitySlider;
    
    private int PaintBrushSize = 20;
    private int Density = 50;
    private float Size = 1;
    private float ScaleDiff = 0.5f;
    private bool RandRott = true;
    private bool RandScale = true;
    private GameObject PaintBrushInstance;
    private bool Active = false;
    private GameObject PaintObj;
    private int ObjLodTrans;
    private string Objtag;
    private bool RandScaleOff = false;
    private bool Eraser;

    public void EnableRandRott(bool r)
    {
        RandRott = r;
    }
    public void EnableEraser(bool er)
    {
        if (Eraser)
        {
            DensitySlider.GetComponent<Slider>().interactable = true;
            ScaleSlider.GetComponent<Slider>().interactable = true;
            ScaleToggle.GetComponent<Toggle>().interactable = true;
            RandRotToggle.GetComponent<Toggle>().interactable = true;
        }
        else
        {
            DensitySlider.GetComponent<Slider>().interactable = false;
            ScaleSlider.GetComponent<Slider>().interactable = false;
            ScaleToggle.GetComponent<Toggle>().interactable = false;
            RandRotToggle.GetComponent<Toggle>().interactable = false;
        }
        if (RandScaleOff)
        {
            ScaleToggle.GetComponent<Toggle>().interactable = false;
            ScaleSlider.GetComponent<Slider>().interactable = false;
            ScaleSlider.GetComponent<Slider>().value = 1;
        }
        Eraser = er;
    }
    public void EnableRandScale(bool s)
    {
        RandScale = s;
    }
    public void SetPaintBrushSize(float p)
    {
        PaintBrushSize = (int)p;
    }
    public void SetDensity(float d)
    {
        Density = (int)d;
    }
    public void SetSize(float s)
    {
        Size = s;
    }
    public void StartBrush((GameObject g,int t,string st,bool sf) T)
    {
        ObjLodTrans = T.t;
        PaintObj = T.g;
        Objtag = T.st;
        RandScaleOff = T.sf;
        if (RandScaleOff)
        {
            ScaleToggle.GetComponent<Toggle>().interactable = false;
            ScaleSlider.GetComponent<Slider>().interactable = false;
            ScaleSlider.GetComponent<Slider>().value = 1;
        }
        else
        {
            ScaleToggle.GetComponent<Toggle>().interactable = true;
            ScaleSlider.GetComponent<Slider>().interactable = true;
        }
        gameObject.SetActive(true);
        Active = true;
    }
    void StopBrush()
    {
        Active = false;
        PaintObj = null;
        gameObject.SetActive(false);
    }
    void CheckInput()
    {
        if (Input.GetMouseButtonDown(01))
        {
            StopBrush();
        }
    }
    void Update()
    {
        CheckInput();

        if (Active)
        {
            
            if (PaintBrushInstance == null)
            {
                PaintBrushInstance = GameObject.Instantiate(PaintBrushObj);
            }
            PaintBrushInstance.transform.localScale = new Vector3(PaintBrushSize, 1, PaintBrushSize);
            Vector3 mousePosition = Input.mousePosition;
            Ray R = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitdatta;
            if (Physics.Raycast(R, out hitdatta))
            {
                PaintBrushInstance.transform.position = hitdatta.point;
            }
            if (Input.GetMouseButton(00) && StatsHandler.OverUi == false && Density > 1 && Eraser == false)
            {
                Paint(hitdatta);
            }
            if (Input.GetMouseButtonDown(00) && StatsHandler.OverUi == false && Density == 1 && Eraser == false)
            {
                Paint(hitdatta);
            }
            if (Input.GetMouseButton(00) && StatsHandler.OverUi == false && Eraser == true)
            {
                Erase(hitdatta);
            }
        }
        else
        {
            if (PaintBrushInstance != null)
            {
                Destroy(PaintBrushInstance);
            }
        }
    }
    void Paint(RaycastHit Hit)
    {
        Collider[] LocPaintedObj = Physics.OverlapSphere(Hit.point, PaintBrushSize / 2);
        int PaintZoneObj = 0;
        foreach (Collider c in LocPaintedObj)
        {
            if (c.gameObject.GetComponent<TagList>() != null)
            {
                if (c.gameObject.GetComponent<TagList>().HasTag(Objtag) == true)
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
                GameObject BrushActiveObj = GameObject.Instantiate(PaintObj);
                BrushActiveObj.AddComponent<TagList>().AddTag("PlayerDecoration");
                BrushActiveObj.GetComponent<TagList>().AddTag(Objtag);
                BrushActiveObj.transform.position = ObjLocation;
                BrushActiveObj.transform.localScale = new Vector3(Size, Size, Size);
                if (RandRott)
                {
                    BrushActiveObj.transform.rotation = new Quaternion(0, Random.Range(-180, 180), 0, 100);
                }
                if (RandScale && RandScaleOff == false)
                {
                    float tempScaleVar = Size * Random.Range(Size * Size - ScaleDiff, Size * Size + ScaleDiff);
                    Vector3 RandScale = new Vector3(tempScaleVar, tempScaleVar, tempScaleVar);
                    BrushActiveObj.transform.localScale = RandScale;
                }
                if (BrushActiveObj.GetComponent<LODGroup>())
                {
                    DestroyImmediate(BrushActiveObj.GetComponent<LODGroup>());
                }
                LOD l = new LOD();
                l.renderers = new Renderer[1] { BrushActiveObj.GetComponent<MeshRenderer>() };
                l.screenRelativeTransitionHeight = BrushActiveObj.transform.localScale.x / ObjLodTrans;
                LOD[] r = new LOD[] { l };
                BrushActiveObj.AddComponent<LODGroup>().SetLODs(r);
            }
        }
    }
    void Erase(RaycastHit Hit)
    {
        Collider[] LocPaintedObj = Physics.OverlapSphere(Hit.point, PaintBrushSize / 2);
        foreach(Collider L in LocPaintedObj)
        {
            if(L.gameObject.GetComponent<TagList>() != null)
            {
                if (L.gameObject.GetComponent<TagList>().HasTag(Objtag))
                {
                    Destroy(L.gameObject);
                }
            }
        }
    }
    Vector3 RandomLocation(RaycastHit Hit)
    {
        Vector3 GroundPosition = new Vector3(-99, -99, -99);
        Vector3 HitPos = Hit.point + new Vector3(Random.Range(-PaintBrushSize * 0.3f, PaintBrushSize * 0.3f), 0, Random.Range(-PaintBrushSize * 0.3f, PaintBrushSize * 0.3f));
        RaycastHit hitdatta;
        Ray r = new Ray(HitPos + new Vector3(0, 120, 0), Vector3.down);
        if (Physics.Raycast(r, out hitdatta, 450))
        {
            if (hitdatta.transform.gameObject.CompareTag("Floor")) 
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
}
