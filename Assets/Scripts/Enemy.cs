using UnityEngine;

public class Enemy : MonoBehaviour
{





    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;
    private Vector3 starPos;
    private bool movingRight = true;


    void Start()
    {
        starPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        float leftBound = starPos.x - distance;
        float rightBound = starPos.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
                flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                flip();
            }
        }

    }




    void flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
 