using UnityEngine;

public class Lightzone : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onLightzoneEnter;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player entered lightzone");
            onLightzoneEnter.Raise(this, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited lightzone");
            onLightzoneEnter.Raise(this, false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player is in lightzone");
            onLightzoneEnter.Raise(this, true);
        }
    }
}