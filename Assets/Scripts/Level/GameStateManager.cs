using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public enum GameState
    {
        Running,
        CharactersStopped,
        Paused
    };

    public static GameState currentState = GameState.Running;

    public static void SetGameState(GameState state) => currentState = state;
}
