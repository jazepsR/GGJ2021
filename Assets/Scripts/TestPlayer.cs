using UnityEngine;

namespace DefaultNamespace
{
    public class TestPlayer : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
                transform.position += Vector3.up;
            if (Input.GetKeyDown(KeyCode.A))
                transform.position += Vector3.left;
            if (Input.GetKeyDown(KeyCode.S))
                transform.position += Vector3.down;
            if (Input.GetKeyDown(KeyCode.D))
                transform.position += Vector3.right;
        }
    }
}