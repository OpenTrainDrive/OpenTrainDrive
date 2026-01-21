using System;
using System.Collections.Generic;

public sealed class Bedienaufforderung
{
    public Bedienaufforderung(string signalId, string message, DateTime createdAtUtc)
    {
        SignalId = signalId;
        Message = message;
        CreatedAtUtc = createdAtUtc;
        IsActive = true;
    }

    public string SignalId { get; }
    public string Message { get; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAtUtc { get; }
    public DateTime? AcknowledgedAtUtc { get; private set; }

    public void Acknowledge(DateTime acknowledgedAtUtc)
    {
        if (!IsActive)
        {
            return;
        }

        IsActive = false;
        AcknowledgedAtUtc = acknowledgedAtUtc;
    }
}

public sealed class BedienaufforderungManager
{
    private readonly Dictionary<string, Bedienaufforderung> _active = new(StringComparer.OrdinalIgnoreCase);

    public event Action<Bedienaufforderung>? Raised;
    public event Action<Bedienaufforderung>? Acknowledged;

    public IReadOnlyCollection<Bedienaufforderung> Active => _active.Values;

    public Bedienaufforderung? Raise(string signalId, string message)
    {
        if (string.IsNullOrWhiteSpace(signalId))
        {
            return null;
        }

        var id = signalId.Trim();
        if (_active.TryGetValue(id, out var existing))
        {
            return existing;
        }

        var text = string.IsNullOrWhiteSpace(message) ? $"Bedienaufforderung {id}" : message.Trim();
        var item = new Bedienaufforderung(id, text, DateTime.UtcNow);
        _active[id] = item;
        Raised?.Invoke(item);
        return item;
    }

    public bool Acknowledge(string signalId)
    {
        if (string.IsNullOrWhiteSpace(signalId))
        {
            return false;
        }

        var id = signalId.Trim();
        if (!_active.TryGetValue(id, out var item))
        {
            return false;
        }

        item.Acknowledge(DateTime.UtcNow);
        _active.Remove(id);
        Acknowledged?.Invoke(item);
        return true;
    }
}
