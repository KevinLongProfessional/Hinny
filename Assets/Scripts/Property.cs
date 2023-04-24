using System.Collections.Generic;
using System;
using UnityEngine;


//https://stackoverflow.com/questions/73595753/is-there-a-way-to-easily-bind-data-to-ui-in-runtime
public interface IProperty<T> : IProperty
{
    new event Action<T> ValueChanged;
    new T Value { get; }
}

public interface IProperty
{
    event Action<object> ValueChanged;
    object Value { get; }
}

[Serializable]
public class Property<T> : IProperty<T>
{
    //to do: try putting a get/set here so there's no need to get value directly.
    //Must be possible somehow...

    public event Action<T> ValueChanged;

    event Action<object> IProperty.ValueChanged
    {
        add => valueChanged += value;
        remove => valueChanged -= value;
    }

    [SerializeField]
    private T value;

    public T Value
    {
        get => value;

        set
        {
            if (EqualityComparer<T>.Default.Equals(this.value, value))
            {
                return;
            }

            this.value = value;

            ValueChanged?.Invoke(value);
            valueChanged?.Invoke(value);
        }
    }

    object IProperty.Value => value;

    private Action<object> valueChanged;

    public Property(T value) => this.value = value;

    public static explicit operator Property<T>(T value) => new Property<T>(value);
    public static implicit operator T(Property<T> binding) => binding.value;
}