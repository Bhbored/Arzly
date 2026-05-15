using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arzly.Client.Services
{
    public class ToastService
    {
        public event Action? OnChange;
        private readonly List<ToastItem> items = new();

        public IReadOnlyList<ToastItem> Items => items;

        public void ShowSuccess(string message) => AddToast(message, "success");
        public void ShowError(string message) => AddToast(message, "error");

        private void AddToast(string message, string type)
        {
            var t = new ToastItem { Id = Guid.NewGuid(), Message = message, Type = type };
            items.Add(t);
            Notify();
            _ = RemoveAfterDelay(t.Id, 4000);
        }

        private async Task RemoveAfterDelay(Guid id, int ms)
        {
            await Task.Delay(ms);
            Remove(id);
        }

        public void Remove(Guid id)
        {
            var it = items.FirstOrDefault(i => i.Id == id);
            if (it != null)
            {
                items.Remove(it);
                Notify();
            }
        }

        private void Notify() => OnChange?.Invoke();

        public class ToastItem
        {
            public Guid Id { get; set; }
            public string Message { get; set; } = string.Empty;
            public string Type { get; set; } = "info";
        }
    }
}