using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip eatClip;
    public AudioClip jumpClip;
    public AudioClip throwRopeClip;
    public AudioClip scoreClip;
    private void Start()
    {
        GameEvents.OnWheatDestroyEvent.AddListener(WheatDestroyEvent);
        GameEvents.OnPlayerJumpEvent.AddListener(PlayerJumpEvent);
        GameEvents.OnPlayerThrowRopeEvent.AddListener(PlayerCatchEvent);
        GameEvents.OnCamelKnotEvent.AddListener(CamelKnotEvent);

    }
    private void OnDestroy()
    {
        GameEvents.OnWheatDestroyEvent.RemoveListener(WheatDestroyEvent);
        GameEvents.OnPlayerJumpEvent.RemoveListener(PlayerJumpEvent);
        GameEvents.OnPlayerThrowRopeEvent.RemoveListener(PlayerCatchEvent);
        GameEvents.OnCamelKnotEvent.RemoveListener(CamelKnotEvent);
    }


    private void WheatDestroyEvent(Wheat wheat, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(eatClip, pos);
    }
    private void PlayerJumpEvent()
    {
        AudioSource.PlayClipAtPoint(jumpClip, Vector3.zero);
    }
    private void PlayerCatchEvent()
    {
        AudioSource.PlayClipAtPoint(throwRopeClip, Vector3.zero);
    }
    private void CamelKnotEvent()
    {
        AudioSource.PlayClipAtPoint(scoreClip, Vector3.zero);
    }
}
