using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartInfo : MonoBehaviour
{
    public GameObject moveWith;
    public GameObject wasd,w,a,s,d;
    public GameObject jumpWith;
    public GameObject space;
    public GameObject ropeThrow;
    public GameObject mouse0;
    public GameObject camel;

    private IEnumerator Start()
    {
        if(!PlayerPrefs.HasKey("OpenedBefore"))
        {
            moveWith.SetActive(true);
            wasd.SetActive(true);

            yield return new WaitUntil(delegate { return Input.GetKeyDown(KeyCode.W); });
            w.SetActive(false);
            yield return new WaitUntil(delegate { return Input.GetKeyDown(KeyCode.A); });
            a.SetActive(false);
            yield return new WaitUntil(delegate { return Input.GetKeyDown(KeyCode.S); });
            s.SetActive(false);
            yield return new WaitUntil(delegate { return Input.GetKeyDown(KeyCode.D); });
            d.SetActive(false);

            moveWith.gameObject.SetActive(false);

            jumpWith.SetActive(true);
            space.SetActive(true);
            yield return new WaitUntil(delegate { return Input.GetKeyDown(KeyCode.Space); });
            yield return new WaitForSecondsRealtime(1f);
            jumpWith.SetActive(false);
            space.SetActive(false);

            ropeThrow.SetActive(true);
            mouse0.SetActive(true);
            yield return new WaitUntil(delegate { return Input.GetMouseButtonDown(0); });
            yield return new WaitForSecondsRealtime(1f);
            ropeThrow.SetActive(false);
            mouse0.SetActive(false);

            camel.SetActive(true);
            yield return new WaitForSecondsRealtime(7f);
            camel.SetActive(false);
        }


        yield return new WaitForSecondsRealtime(1);
        GameEvents.OnStartInfoEndEvent?.Invoke();
        PlayerPrefs.SetInt("OpenedBefore", 1);
    }


    
}
