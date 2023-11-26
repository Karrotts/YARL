using Godot;

public partial class Entity : CharacterBody2D
{
    public MovementComponent MovementComponent { get; set; }
    public HealthComponent HealthComponent { get; set; }

    public override void _Ready()
    {
        base._Ready();
        
        // setup Movement Component
        MovementComponent = GetNode<MovementComponent>("Components/MovementComponent");
        MovementComponent.ControllableEntity = this;

        // setup Health Component
        HealthComponent = GetNode<HealthComponent>("Components/HealthComponent");
    }

    /// <summary>
    /// Safely destroys the entity
    /// </summary>
    public void DestroyEntity()
    {
        CallDeferred("queue_free");
    }
}
