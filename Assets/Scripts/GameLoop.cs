using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

public class GameLoop : MonoBehaviour
{

    private void Start()
    {
        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;


    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        Debug.Log(e.Position);
    }

}

