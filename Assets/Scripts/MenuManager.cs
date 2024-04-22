using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jack_Chiu
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

        /// <summary>
        /// 重新遊戲
        /// </summary>
        public void RestartGame()
		{
            // 取得當前遊戲場景的名稱
            String scene = SceneManager.GetActiveScene().name;
            // 載入至當前遊戲場景
            SceneManager.LoadScene(scene);
        }
    }
}
