using UnityEngine;

public class ScreenTransitionAnimation : MonoBehaviour
{
    void Awake()
    {
        AnimateSceneStart();
    }

    public void AnimateSceneStart()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        Vector2 endPosition = Vector2.left * 30;
        Vector2 vector = endPosition - ( Vector2 ) transform.position;
        if ( vector.magnitude < 0.1f )
        {
            gameObject.SetActive( false );
        }

        transform.position = Vector3.MoveTowards( transform.position, endPosition, 80 * Time.smoothDeltaTime );
    }
}