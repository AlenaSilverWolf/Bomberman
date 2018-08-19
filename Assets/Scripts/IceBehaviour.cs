using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IceBehaviour : MonoBehaviour
{
    public Sprite door;
    public Sprite grass;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero" && gameObject.tag == "Door")
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                StartCoroutine(NextLevel(collision.gameObject, "Level"));
        }
        if (collision.gameObject.tag == "Exp0" || collision.gameObject.tag == "Exp1")
            gameObject.GetComponent<Image>().sprite = (gameObject.tag == "Door")? door: grass;
    }

    IEnumerator NextLevel(GameObject col, string txtScene)
    {
        col.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        if (col.GetComponent<Rigidbody2D>() != null)
            Destroy(col.GetComponent<Rigidbody2D>());
        if (col.GetComponent<BoxCollider2D>() != null)
            Destroy(col.GetComponent<BoxCollider2D>());
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadSceneAsync(txtScene);
    }
}