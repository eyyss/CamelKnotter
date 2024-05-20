using System.Collections.Generic;
using UnityEngine;

public class WheatManager : MonoBehaviour
{
    public List<Wheat> wheatList = new List<Wheat> ();
    public ParticleSystem wheatDestroyEffect;

    private void Awake()
    {
        var list = transform.GetComponentsInChildren<Wheat>();
        foreach (var item in list)
        {
            wheatList.Add(item);
        }
        
    }
    private void Start()
    {
        GameEvents.OnWheatDestroyEvent.AddListener(WheatDestroyEvent);
    }
    private void OnDestroy()
    {
        GameEvents.OnWheatDestroyEvent.RemoveListener(WheatDestroyEvent);
    }

    private void WheatDestroyEvent(Wheat wheat,Vector3 pos)
    {
        Instantiate(wheatDestroyEffect, pos, Quaternion.identity);
        wheatList.Remove(wheat);
        if (wheatList.Count < 1)
        {
            //gameend
            GameEvents.OnGameLose?.Invoke();
        }
    }
}
