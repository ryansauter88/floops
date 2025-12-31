using Godot;

public partial class Singularity : Node
{
    private PackedScene menuLevel;
	private PackedScene testLevel;

    public override void _Ready()
    {
        testLevel = ResourceLoader.Load<PackedScene>("res://Scenes/test_level.tscn");
        menuLevel = ResourceLoader.Load<PackedScene>("res://Scenes/main_menu.tscn");
    }

    public void ChangeSceneTestLevel()
    {
        GetTree().ChangeSceneToPacked(testLevel);
    }

    public void ChangeSceneMenuLevel()
    {
        GetTree().ChangeSceneToPacked(menuLevel);
    }
}