using UnityEngine;

public class ScreenTransitionAnimation : MonoBehaviour
{
    public delegate void OnSceneEndCallback();

    private Vector2 endPosition = Vector2.left * 30;
    private OnSceneEndCallback sceneEndCallback = null;

    public void AnimateSceneEnd( OnSceneEndCallback callback )
    {
        sceneEndCallback = callback;
        transform.position = Vector2.right * 30;
        endPosition = Vector2.zero;
        gameObject.SetActive( true );
    }

    public bool IsTransitionOver()
    {
        return !gameObject.activeSelf;
    }

    private void Awake()
    {
        AnimateSceneStart();
    }

    private void AnimateSceneStart()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update()
    {
        Vector2 vector = endPosition - ( Vector2 ) transform.position;
        if ( vector.magnitude < 0.1f )
        {
            if ( sceneEndCallback != null )
            {
                sceneEndCallback();
            }
            else
            {
                gameObject.SetActive( false );
            }
        }

        transform.position = Vector3.MoveTowards( transform.position, endPosition, 100 * Time.smoothDeltaTime );
    }
}