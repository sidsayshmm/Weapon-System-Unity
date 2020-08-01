using System;

[Flags]
public enum AvailableShootModes
{
    Normal = (1<<0),
    Burst = (1<<1),
    Single = (1<<2)
}