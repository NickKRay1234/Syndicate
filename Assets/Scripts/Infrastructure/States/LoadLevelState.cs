using Infrastructure.Factory;
using Logic;
using Scripts.CameraLogic;
using Scripts.Infrastructure;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            _gameFactory.CreateHud();
            CameraFollow(hero);
            _stateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }

        public void Update()
        {
        }

        public void Exit() => _curtain.Hide();
        public void Enter()
        {
            throw new System.NotImplementedException();
        }
    }
}