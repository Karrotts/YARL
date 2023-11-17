using Godot;
using System;

public partial class DamageText : Node2D
{
    [Export]
    public AnimatedSprite2D FirstDigit { get; set; }

    [Export]
    public AnimatedSprite2D SecondDigit { get; set; }

    [Export]
    public AnimatedSprite2D ThirdDigit { get; set; }

    [Export]
    public AnimatedSprite2D FourthDigit { get; set; }

    [Export]
    public int Number {
        get { return _number;  }
        set
        {
            _number = value > 0 ? value : 0;
        }
    }

    private int _number;
    private bool _wasInit = false;

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!_wasInit)
        {
            HideDigits();
            SetDigits();
            GetNode<AnimationPlayer>("AnimationPlayer").Play();
            _wasInit = true;
        }
    }

    private void HideDigits()
    {
        if (_number < 1000) FourthDigit.Hide();
        if (_number < 100) ThirdDigit.Hide();
        if (_number < 10) SecondDigit.Hide();
        if (_number < 1) FirstDigit.Hide(); 
    }

    public void SetDigits()
    {
        int baseNum = 10;
        AnimatedSprite2D[] sprites = { FirstDigit, SecondDigit, ThirdDigit, FourthDigit};
        for (int i = 0; i < 4; i++)
        {
            SetNumber(sprites[i], Number % baseNum);
            Number /= baseNum;
        }
    }

    private void SetNumber(AnimatedSprite2D sprite2D, int number)
    {
        if (number > 9) throw new Exception("Number was set to a digit greater than 9!");
        sprite2D.Frame = number;
    }

}
