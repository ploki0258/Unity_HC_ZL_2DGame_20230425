using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jack
{
    /// <summary>
    /// 選單管理器
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        /// <summary>
        /// 開始遊戲
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// 離開遊戲
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
