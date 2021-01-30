using UnityEngine.SceneManagement;

namespace GameSystem
{
    public static class GameScene
    {
        public static void RestartGame()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        public static void LoadScene(string sceneName)
        {
            GameStateManager.CurrentGameState.Value = GameState.Loading;
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}