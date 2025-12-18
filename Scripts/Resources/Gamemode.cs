using Godot;

[GlobalClass]
public partial class Gamemode : Resource
{
    [Export] int gameTime = 300;
    [Export] int playersPerTeam = 2;
    // how tf am i gonna do player positions i guess just vectors?
}
