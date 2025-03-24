using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour
{
    Vector2 dir;

    List<Transform> segments = new List<Transform>();
    [SerializeField] Transform segmentPrefab;

    void Start()
    {
        Time.timeScale = 0.25f;
        dir = Vector2.right;

        segments.Add(transform);

        for (int i = 0; i < 10; i++)
        {
            Grow();
        }
    }

    void Update()
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) && dir.x!=0)
            {
                dir = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && dir.x!=0)
            {
                dir = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && dir.y!=0)
            {
                dir = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && dir.y!=0)
            {
                dir = Vector2.left;
            }
        }

    private void FixedUpdate()
        {

            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            float x = Mathf.Round(transform.position.x) + dir.x;
            float y = Mathf.Round(transform.position.y) + dir.y;

            transform.position = new Vector2(x, y);

        }

        void Grow()
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.position = segments[segments.Count - 1].position;
            segments.Add(segment);
        }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Food") Grow();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(GetComponent<Snake>());
    }
}

