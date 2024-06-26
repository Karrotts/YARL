using Godot;

public partial class HealthComponent : Node
{
    #region SIGNALS
    [Signal]
    public delegate void HealthZeroEventHandler();

    [Signal]
    public delegate void OnDamageTakenEventHandler(int damage);

    [Signal]
    public delegate void OnHealthGainEventHandler();
    #endregion

    #region EXPORTS
    [Export(PropertyHint.Range, "0,10000")]
    public int MaxHealth { get; set; } = 10;

    [Export(PropertyHint.Range, "0,10000")]
    public int StartHealth { get; set; } = 10;

    [Export]
    public bool TotalInvulnerability { get; set; } = false;

    [Export(PropertyHint.Range, "0, 10")]
    public float InvulnerabilityTime { 
        get { return _invulnerabilityTime; } 
        set 
        {
            _invulnerabilityTime = value <= 10 ? value > 0 ? value : 0 : 10;
            if (_invulnerabilityTimer != null)
            {
                _invulnerabilityTimer.WaitTime = _invulnerabilityTime;
            }
        } 
    }
    #endregion

    public bool HasTemporaryInvulnerability { get; set; } = false;

    public bool IsInvulnerable {
        get { return TotalInvulnerability || HasTemporaryInvulnerability; }    
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set 
        {
            // deal no damage if invulnerable
            if (IsInvulnerable) return;
            if (value <= 0)
            {
                _currentHealth = 0;
                EmitSignal(SignalName.HealthZero);
            }
            else if (value > _currentHealth)
            {
                // set health to max health if value is above the limit
                _currentHealth = value > MaxHealth ? MaxHealth : value;
                EmitSignal(SignalName.OnHealthGain);
            }
            else if (value < _currentHealth)
            {
                _currentHealth = value;

                // set entity to be invulnerable and start the timer
                _invulnerabilityTimer.Start();
                HasTemporaryInvulnerability = true;
            }
        }
    }

    private int _currentHealth = 0;
    private float _invulnerabilityTime = 0;
    private Timer _invulnerabilityTimer = null;

    public override void _Ready()
    {
        CurrentHealth = StartHealth;

        // set up invulnerability timer
        _invulnerabilityTimer = GetNode<Timer>("InvulnerabilityTimer");
        _invulnerabilityTimer.WaitTime = _invulnerabilityTime;
        _invulnerabilityTimer.OneShot = true;
        _invulnerabilityTimer.Autostart = false;
        _invulnerabilityTimer.Timeout += () => HasTemporaryInvulnerability = false;
    }

    /// <summary>
    /// Deals an amount of damage, returns the remaining amount of health after damage is dealt
    /// </summary>
    /// <param name="amount">Amount of damage to deal</param>
    /// <param name="blocking">Triggers the invincibility timer if true</param>
    /// <returns>Health after damage dealt</returns>
    public int DealDamage(int amount, bool blocking = true)
    {
        if (_currentHealth <= 0) return 0;

        if (blocking)
            CurrentHealth -= amount;
        else
            DealNonBlockingDamage(amount);

        if (amount > 0) EmitSignal(SignalName.OnDamageTaken, amount);
        
        return CurrentHealth;
    }

    private void DealNonBlockingDamage(int amount)
    {
        int old = _currentHealth;
        _currentHealth -= amount;
        if (_currentHealth == 0) EmitSignal(SignalName.HealthZero);
        if (old < _currentHealth) EmitSignal(SignalName.OnHealthGain);
    }
}
