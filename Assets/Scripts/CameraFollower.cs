using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hero");
        transform.position = player.transform.position;
    }
    void Update()
    {
        transform.position = player.transform.position;
    }
}