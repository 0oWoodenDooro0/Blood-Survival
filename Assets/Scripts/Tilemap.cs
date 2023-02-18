using UnityEngine;

public class Tilemap : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area")) return;

        var playerPosition = Game.Instance.player.transform.position;
        var position = transform.position;
        var diffX = Mathf.Abs(playerPosition.x - position.x);
        var diffY = Mathf.Abs(playerPosition.y - position.y);

        var playerDirection = Game.Instance.playerScript.inputVector;
        var directionX = playerDirection.x < 0 ? -1 : 1;
        var directionY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * directionX * 80);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * directionY * 80);
                }

                break;
        }
    }
}