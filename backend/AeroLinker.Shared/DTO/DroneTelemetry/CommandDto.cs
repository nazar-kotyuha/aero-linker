namespace AeroLinker.Shared.DTO.Drone;

public enum Option
{
    Up,
    Down,
    Left,
    Right,
    Ascend,
    Descend,
    Accelerate,
    Decelerate 
}

public class CommandDto
{
    public Option Option { get; set; }
    public double Step { get; set; }
}