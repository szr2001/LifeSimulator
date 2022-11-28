using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnArea;
    public GameObject SpawnObject;
    public int SpawnAmmount;
    void Start()
    {
        for (int i = 0; i < SpawnAmmount; i++)
        {
            GameObject ReadyToSpawn = GameObject.Instantiate(SpawnObject);
            Vector3 NewObjectLocation= new Vector3(0,0.25f ,0) + getRandomPosition();
            ReadyToSpawn.transform.position = NewObjectLocation;
            Quaternion RandRot = new Quaternion(0,Random.Range(-180, 180), 0, 100);
            ReadyToSpawn.transform.rotation = RandRot;
        }
        Destroy(gameObject);
    }

    private Vector3 getRandomPosition()
    {
        BoxCollider boxCollider = SpawnArea != null ? SpawnArea.GetComponent<BoxCollider>() : null;

        return new Vector3(Random.Range(SpawnArea.transform.position.x - SpawnArea.transform.localScale.x * boxCollider.size.x * 0.5f,
                                                    SpawnArea.transform.position.x + SpawnArea.transform.localScale.x * boxCollider.size.x * 0.5f),
                           SpawnArea.transform.position.y,
                           Random.Range(SpawnArea.transform.position.z - SpawnArea.transform.localScale.z * boxCollider.size.z * 0.5f,
                                                    SpawnArea.transform.position.z + SpawnArea.transform.localScale.z * boxCollider.size.z * 0.5f));
        

    }

}
