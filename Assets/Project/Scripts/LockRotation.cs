using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public GameObject player;
    public Vector3 camOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        //this.transform.position = player.gameObject.transform.position + camOffset;
        this.transform.position = new Vector3(player.gameObject.transform.position.x + camOffset.x,player.gameObject.transform.position.y + camOffset.y, player.gameObject.transform.position.z + camOffset.z);
    }
}
