using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*public GameObject bomb;
    public GameObject explosion;
    public GameObject expUp1;
    public GameObject expDown1;
    public GameObject expLeft1;
    public GameObject expRight1;
    float speed = 5;*/

    /*void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            GetComponent<Animator>().SetInteger("Direction", 1);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            GetComponent<Animator>().SetInteger("Direction", 2);
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            GetComponent<Animator>().SetInteger("Direction", 3);
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            GetComponent<Animator>().SetInteger("Direction", 4);
        else
            GetComponent<Animator>().SetInteger("Direction", 0);

        if (Input.GetKeyDown(KeyCode.Space))
            CreateBomb();

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if(gameObject.GetComponent<Rigidbody2D>() != null)
            GetComponent<Rigidbody2D>().MovePosition(new Vector2((transform.position.x + move.x * speed * Time.deltaTime), (transform.position.y + move.y * speed * Time.deltaTime)));
    }*/
}