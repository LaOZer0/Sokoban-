public class GameplayPresenter
{
	private IGameplayModel _gameplayModel = null;
	private IGameplayView _gameplayView = null;

	public GameplayPresenter(IGameplayModel gameplayModel, IGameplayView gameplayView)
    {
        _gameplayModel = gameplayModel;
        _gameplayView = gameplayView;

        _gameplayView.CycleFinished += ViewModelUpdate;
        _gameplayView.PlayerMoved += ViewModelMovePlayer;
        _gameplayModel.Updated += ModelViewUpdate;
    }

    private void ViewModelMovePlayer(object sender, ControlsEventArgs e)
    {
        _gameplayModel.MovePlayer(e.Direction);
    }

    private void ModelViewUpdate(object sender, ControlsEventArgs e)
    {
        _gameplayView.LoadGameCycleParametrs(e.PlayerPos);
    }

    private void ViewModelUpdate(object sender, ControlsEventArgs e)
    {
        _gameplayModel.Update();
    }
}