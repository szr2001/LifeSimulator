using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private GameObject SpawnObj;
    private void Update()
    {
        if (SpawnObj == null) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 MouseScreenPosition = Input.mousePosition;
            Ray R = Camera.main.ScreenPointToRay(MouseScreenPosition);
            RaycastHit hitdatta;
            if (Physics.Raycast(R, out hitdatta))
            {
                GameObject newObj = GameObject.Instantiate(SpawnObj);
                newObj.transform.position = hitdatta.point;
                Quaternion RandRot = new Quaternion(0, Random.Range(-180, 180), 0, 100);
                newObj.transform.rotation = RandRot;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnObj = null;
        }
    }
    public void SetSpawnedObj(GameObject Obj)
    {
       StartCoroutine(TurnOnSpawner(Obj));
    }
    public IEnumerator TurnOnSpawner(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        SpawnObj = obj;
    }
}
