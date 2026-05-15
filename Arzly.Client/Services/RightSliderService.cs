using Microsoft.AspNetCore.Components;

namespace Arzly.Client.Services
{
    public class RightSliderService
    {
        public bool IsOpen { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public Type? ComponentType { get; private set; }
        public IDictionary<string, object>? Parameters { get; private set; }

        public event Action? OnChange;

        public void OpenComponent(Type componentType, IDictionary<string, object>? parameters = null, string? title = null)
        {
            ComponentType = componentType;
            Parameters = parameters;
            if (title != null) Title = title;
            IsOpen = true;
            Notify();
        }

        public void Close()
        {
            IsOpen = false;
            Notify();
        }

        public void SetIsOpen(bool value)
        {
            IsOpen = value;
            Notify();
        }

        private void Notify() => OnChange?.Invoke();
    }
}