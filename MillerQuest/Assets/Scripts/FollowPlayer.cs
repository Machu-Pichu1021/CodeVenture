using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void LateUpdate()
    {
        GameObject player = Player.instance.gameObject;
        if (player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, transform.position.z);
    }
}