using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour
{
    public Sprite player1_Win;
    public Sprite player2_Win;
    public Image winImage;
    private bool isWin = false;
    public GameObject panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Result(string name)
    {
        panel.SetActive(true);
        if(name == "Player1" && !isWin)
        {
            winImage.sprite = player1_Win;
            isWin = true;
        }
        else if(name == "Player2" && !isWin)
        {
            winImage.sprite = player2_Win;
            isWin = true;
        }
    }
}
