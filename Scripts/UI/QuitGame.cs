using Godot;

public partial class QuitGame : Control
{
    public void QuitButtonPress()
    {
        GetTree().Quit();
    }
}