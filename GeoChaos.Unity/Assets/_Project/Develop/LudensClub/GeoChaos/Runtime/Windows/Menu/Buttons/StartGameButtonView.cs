﻿using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Menu
{
  [AddComponentMenu(ACC.Names.START_GAME_BUTTON_VIEW)]
  public class StartGameButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
      _gameStateMachine.SwitchState<GameplayGameState>().Forget();
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(OnClick);
    }

    private void Reset()
    {
      _button = GetComponent<Button>();
    }
  }
}