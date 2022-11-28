using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureDetailsInfo : MonoBehaviour
{

    public Text age;
    public Text hunger;
    public Text behavior;
    public Text health;
    public Image Gender;
    public Sprite GFemale;
    public Sprite GMale;
    public Sprite GEmpty;
    // Update is called once per frame
    private GameObject ClickedObj;
    void Update()
    {
        updateCreatureDetails();
        ClickOnObjects();
    }
    void updateCreatureDetails()
    {
        if (ClickedObj != null)
        {
            Creature C = ClickedObj.GetComponent<Creature>();
            age.text = C.age.ToString();
            hunger.text = Mathf.RoundToInt(C.hunger).ToString();
            behavior.text = C.CreatureBehavior.ToString();
            health.text = Mathf.RoundToInt(C.health).ToString();
            if (Gender.sprite == GEmpty)
            {
                switch (C.CGenes.CreatureGender)
                {
                    case Creature.Gender.female:
                        Gender.sprite = GFemale;
                        break;
                    case Creature.Gender.male:
                        Gender.sprite = GMale;
                        break;
                    default:
                        Gender.sprite = GEmpty;
                        break;
                }
            }
        }
        else
        {
            if (age.text != "?")
            {
                age.text = "?";
                hunger.text = "?";
                behavior.text = "?";
                health.text = "?";
                Gender.sprite = GEmpty;
            }
        }
    }
    void ClickOnObjects()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 MouseScreenPosition = Input.mousePosition;
            Ray R = Camera.main.ScreenPointToRay(MouseScreenPosition);
            RaycastHit hitdatta;
            //SCHIMBA CU INTERFACE
            if (Physics.Raycast(R, out hitdatta))
            {
                if (hitdatta.transform.CompareTag("DumbAnimal"))
                {
                    if (ClickedObj != null)
                    {
                        ClickedObj.GetComponent<DumbCreature>().Selected.SetActive(false);
                    }
                    ClickedObj = hitdatta.collider.gameObject;
                    ClickedObj.GetComponent<DumbCreature>().Selected.SetActive(true);
                }
                else
                {
                    if (ClickedObj != null)
                    {
                        ClickedObj.GetComponent<DumbCreature>().Selected.SetActive(false);
                        ClickedObj = null;
                    }
                }
            }

        }
    }
}
