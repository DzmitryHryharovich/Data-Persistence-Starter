using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuControl : MonoBehaviour
{
    public InputField playerName;
    public Text bestScore;
    public Text bestScoreAll;
    public GameObject topPlayersScrollView;

    private void Start()
    {      
        if (SavaAndLoad.Instance.topPlayers.Count != 0)
        {
            SavaAndLoad.Instance.topPlayers.Sort((s1, s2) => s2.score.CompareTo(s1.score));
            int num = 1;
            foreach (SavaAndLoad.Player player in SavaAndLoad.Instance.topPlayers)
            {
                bestScore.text += $"{num}:{player.name} {" "} - {" "}{player.score}\n";
                num++;
            }
            bestScoreAll.text = bestScore.text;
            if (SavaAndLoad.Instance.topPlayers.Count > 100) SavaAndLoad.Instance.topPlayers.Remove(SavaAndLoad.Instance.topPlayers[101]);
        }
    }
    public void StartGame()
    {
        if (playerName.text.Length == 0) playerName.text = "NONAME";
        SavaAndLoad.Instance.topPlayers.Add(new SavaAndLoad.Player());
        SavaAndLoad.Instance.topPlayers[SavaAndLoad.Instance.topPlayers.Count - 1].name = playerName.text;
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        SavaAndLoad.Instance.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }
    public void TopPlayersScrollView()
    {
        if (topPlayersScrollView.activeInHierarchy == false) topPlayersScrollView.SetActive(true);
        else topPlayersScrollView.SetActive(false);
    }
}
