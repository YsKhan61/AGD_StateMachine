using UnityEngine;

namespace StatePattern.Utilities
{
    public class GenericDataSO<T> : ScriptableObject
    {
        public T Value { get; private set;}
        public event System.Action<T> OnValueChanged;

        public void SetValue(T value)
        {
            Value = value;
            OnValueChanged?.Invoke(Value);
        }
    }
}
