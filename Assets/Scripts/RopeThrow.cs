using System.Collections;
using UnityEngine;

public class RopeThrow : MonoBehaviour
{
    private float timer;
    public float throwRate = 2;
    private Camera cam;
    public LineRenderer line;
    public SpriteRenderer camelKnot;

    public bool throwing;
    public bool pull;
    private Vector3 mousePosition;
    private Vector3 targetPosition;
    private Camel targetCamel;
    public ParticleSystem camelDestroyEffect;
    private void Awake()
    {
        cam = Camera.main;
        timer = throwRate;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>throwRate && !pull && !throwing && Input.GetMouseButtonDown(0))
        {
            timer = 0;
            Throw();
        }

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

    }
    private void Throw()
    {
        Debug.Log("Throw Rope");
        targetPosition = mousePosition;
        throwing = true;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        line.gameObject.SetActive(true);
        GameEvents.OnPlayerThrowRopeEvent?.Invoke();
    }

    private void LateUpdate()
    {
        if (throwing)
        {
            line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), targetPosition, Time.deltaTime * 10));
            float distance = Vector2.Distance(line.GetPosition(1), targetPosition);
            if (distance < 0.1)
            {
                throwing = false;
                pull = true;
                var hit = Physics2D.OverlapCircle(targetPosition, 0.4f);
                if (hit != null)
                {
                    Debug.Log(hit);
                    if (hit.TryGetComponent(out Camel camel))
                    {
                        targetCamel = camel;
                        targetCamel.PlayerCatch();
                        camelKnot.gameObject.SetActive(true);
                        camelKnot.transform.position = camel.transform.position;
                    }
                }

            }
        }

        if (pull)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1,Vector3.MoveTowards(line.GetPosition(1),transform.position,Time.deltaTime*9));
            float distance = Vector2.Distance(transform.position,line.GetPosition(1));
            if (targetCamel != null)
            {
                camelKnot.transform.position = line.GetPosition(1);
                targetCamel.transform.position = line.GetPosition(1);
            }
            if (distance < 0.1)
            {
                camelKnot.gameObject.SetActive(false);
                line.gameObject.SetActive(false);
                if (targetCamel != null)
                {
                    Destroy(targetCamel.gameObject);
                    Instantiate(camelDestroyEffect, transform.position, Quaternion.identity);
                    GameEvents.OnCamelKnotEvent?.Invoke();
                }
                
                pull = false;
                targetCamel = null;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mousePosition, 0.4f);
    }

    public bool Action()
    {
        if (throwing) return true;
        return false;
    }

}
