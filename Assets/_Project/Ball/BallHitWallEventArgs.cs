using UnityEngine;

/// <summary>
/// A helper class to define EventArgs for the event system.
/// </summary>
public class BallHitWallEventArgs : System.EventArgs
{
    public Vector2 SenderPosition { get; set; }
    public GameObject OtherGameObject { get; set; }
}
