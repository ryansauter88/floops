using Godot;

public partial class JoinGame : Control
{
    Singularity sceneSwapper;

    public override void _Ready()
    {
        sceneSwapper = GetTree().Root.GetNode<Singularity>("Singularity");
    }

    public void JoinButtonPress()
    {
        // CREATE A TRANSITION EFFECT ???
        // autoloader.swap scene 
        sceneSwapper.ChangeSceneTestLevel();
        // grab the level object (test level)
        // work with multiplayer setup to establish authority (either server or host)
        // tell authority to add player
        // if not done so already, AUTHORITY to initiate "in lobby" game-state (maybe it could be a diff game-mode core? maybe make a whole set of game states?)
    }
}