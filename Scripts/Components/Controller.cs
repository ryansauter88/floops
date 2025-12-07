using Godot;

public partial class Controller : Node {
    
    /* 
    here is where the inputs are held for unit behavior
    these are functions called by the state machine when attempting an action
    information about the inputs get passed here from the "brain" (the unit's main script)
        for example, instead of calling an input function, GetMovementDirection will store a
        variable which is modified by the main script, and then gets returned to the state for
        processing physics and attempting to act (denied if state logic determines unable).
    */
    
    public float moveInput = 0;

    public bool attackPress = false;

    public bool dashPress = false;

    public float GetMovementFloat() {
        return moveInput;
    }
    public bool WantsAttack() {
        return attackPress;
    }
    public bool WantsDash() {
        return dashPress;
    }
}