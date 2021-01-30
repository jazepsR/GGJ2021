using UnityEngine;

namespace GameSystem
{
    public class Init : MonoBehaviour
    {
        [SerializeField] private string nextScene;
    
        private void Start()
        {
            GameScene.LoadScene(nextScene);
        }
    }
}