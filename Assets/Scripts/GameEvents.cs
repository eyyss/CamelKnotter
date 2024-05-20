using UnityEngine;
using UnityEngine.Events;

public static class GameEvents 
{
    //player
    public static UnityEvent OnPlayerJumpEvent = new UnityEvent(); 
    public static UnityEvent OnPlayerThrowRopeEvent = new UnityEvent();

    public static UnityEvent<Wheat,Vector3> OnWheatDestroyEvent = new UnityEvent<Wheat, Vector3>();
    public static UnityEvent OnCamelKnotEvent = new UnityEvent();
    public static UnityEvent OnStartInfoEndEvent = new UnityEvent();
    public static UnityEvent OnGameLose = new UnityEvent();
}
