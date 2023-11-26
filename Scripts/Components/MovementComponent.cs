using Godot;
using System;

public partial class MovementComponent : Node
{
    #region SIGNALS
    [Signal]
    public delegate void OnStartMovingEventHandler();

    [Signal]
    public delegate void OnStopMovingEventHandler();
    #endregion

    #region EXPORTS
    [Export]
    [ExportGroup("Entity")]
    public bool IsPlayable { get; set; }

    [Export]
    [ExportGroup("Entity")]
    public CharacterBody2D ControllableEntity;

    [Export(PropertyHint.Range, "1,10000")]
    [ExportGroup("Stats")]
    public float StartingSpeed = 10f;

    [Export(PropertyHint.Range, "1,10000")]
    [ExportGroup("Stats")]
    public float MaxSpeed = 10f;

    [Export]
    public bool ForceStopMovement = false;
    #endregion

    /// <summary>
    /// Temporarily stops the movement of the entity. Use this for any movement blockers.
    /// </summary>
    public bool TemporaryStopMovement = false;

    /// <summary>
    /// True if the entity is currently able to move.
    /// </summary>
    public bool CanMove
    {
        get { return !ForceStopMovement && !TemporaryStopMovement; }
    }

    /// <summary>
    /// Current speed multiplier of the entity
    /// </summary>
    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set
        {
            _currentSpeed = value <= 0f ? 0f : value >= MaxSpeed ? MaxSpeed : value;
        }
    }
    
    /// <summary>
    /// True if the entity is currently moving. Setting triggers signal OnStart or OnStop.
    /// </summary>
    public bool IsMoving
    {
        get { return _isMoving; }
        set 
        { 
            if (_isMoving != value)
            {
                if (_isMoving)
                {
                    EmitSignal(SignalName.OnStartMoving);
                }
                else
                {
                    EmitSignal(SignalName.OnStopMoving);
                }
            }
            _isMoving = value;
        }
    }

    private float _currentSpeed;
    private bool _isMoving;

    public override void _Ready()
    {
        CurrentSpeed = StartingSpeed;
        if (IsPlayable && ControllableEntity == null)
            throw new Exception("Movement Component was marked playable but controllable entity was null!");
    }

    public override void _Process(double delta)
    {
        HandlePlayerControls();
    }

    /// <summary>
    /// Applys the speed multipler to the movement vector
    /// </summary>
    /// <param name="movementVector">Movement vector to apply speed to.</param>
    /// <returns>Movement vector with speed applied.</returns>
    public Vector2 ApplySpeed(Vector2 movementVector)
    {
        Vector2 movement = movementVector * _currentSpeed;
        _isMoving = movement != Vector2.Zero;
        return movementVector * _currentSpeed;
    }

    /// <summary>
    /// Moves the entity in a direction
    /// </summary>
    /// <param name="movementVector"></param>
    public void Move(Vector2 movementVector, bool overrideDisabledMovement = false)
    {
        if (CanMove || overrideDisabledMovement)
        {
            ControllableEntity.Velocity = ApplySpeed(movementVector);
            ControllableEntity.MoveAndSlide();
        }
    }

    /// <summary>
    /// Applies a force in a direction, disabling the movement for a specified period.
    /// </summary>
    /// <param name="direction">Direction to apply force to</param>
    /// <param name="forceAmount">Force amount</param>
    /// <param name="disableDuration">Time until can move again</param>
    public void ApplyCounterForce(Vector2 direction, float forceAmount, float disableDuration = 0.2f)
    {
        TemporaryDisableMovement(disableDuration);
        ControllableEntity.Velocity = direction * forceAmount;
        ControllableEntity.MoveAndSlide();
        
    }

    private void HandlePlayerControls()
    {
        if (IsPlayable && CanMove)
        {
            Vector2 movementVector = Input.GetVector("movement_left", "movement_right", "movement_up", "movement_down");
            ControllableEntity.Velocity = ApplySpeed(movementVector);
            ControllableEntity.MoveAndSlide();
        }
    }

    private async void TemporaryDisableMovement(float length)
    {
        TemporaryStopMovement = true;
        await ToSignal(GetTree().CreateTimer(length), SceneTreeTimer.SignalName.Timeout);
        TemporaryStopMovement = false;
    }
}
