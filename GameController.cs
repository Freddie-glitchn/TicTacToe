using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public int whoTurn; //0 = x and 1=o
    public int turnCount;//counts the number of turn played
    public Sprite[] playerIcons;//0 = x and 1=y icon
    public GameObject[] turnIcons;//displays whose turn it is
    public Button[] tictactoeSpaces;// playable space for our game
    public int[] markedSpaces;//ID's which space is marked by which player
    public Text winnerText;
    public GameObject[] winningLine;
    public GameObject winnerPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    public Text xPlayerScoreText;
    public Text oPlayerScoreText;
    public Button xPlayerButton;
    public Button oPlayerButton;
    public GameObject catImage;
    public AudioSource buttonClickedAudio;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for(int i = 0; i < 9; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }

        for(int i = 0; i < 9; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TicTacToeButton(int whichNumber)
    {
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;
        tictactoeSpaces[whichNumber].image.sprite =playerIcons[whoTurn];
        tictactoeSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoTurn + 1;
        turnCount++;

        if(turnCount > 4)
        {
            bool isWinner = winnerCheck();
            if(turnCount == 9 && isWinner == false)
            {
                CatDraw();
            }
        }

        if(whoTurn == 0)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    bool winnerCheck()
    {
        int sol1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int sol2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int sol3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int sol4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int sol5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int sol6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int sol7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int sol8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solns = new int[] {sol1, sol2, sol3, sol4, sol5, sol6, sol7, sol8};
        for(int i = 0; i < solns.Length; i++)
        {
            if(solns[i] == 3 * (whoTurn + 1))
            {
                WinnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if(whoTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Player X wins!";
        }
        else if(whoTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Player O Wins!";
        }
        winningLine[indexIn].SetActive(true);
        
    }

    public void Rematch()
    {
        GameSetup();
        for(int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        xPlayerButton.interactable = true;
        oPlayerButton.interactable = true;
        catImage.SetActive(false);
    }

    public void newGame()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";
    }

    public void swithPlayer(int whichPlayerStart)
    {
        if(whichPlayerStart == 0)
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if(whichPlayerStart == 1)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    void CatDraw()
    {
        winnerPanel.SetActive(true);
        catImage.SetActive(true);
        winnerText.text = "CAT/DRAW";
    }

    public void playButtonClick()
    {
        buttonClickedAudio.Play();
    }
}
