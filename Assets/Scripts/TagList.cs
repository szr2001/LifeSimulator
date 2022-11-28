using System.Collections.Generic;
using UnityEngine;
public class TagList : MonoBehaviour
{
    public List<string> Taggs = new List<string>();

    public bool HasTag(string tag)
    {
        foreach(string s in Taggs)
        {
            if(s == tag)
            {
                return true;
            }
        }
        return false;
    }
    public void AddTag(string tag)
    {
        Taggs.Add(tag);
    }
}
