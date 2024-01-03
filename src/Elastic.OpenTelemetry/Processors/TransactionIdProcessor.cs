using OpenTelemetry;

using System.Diagnostics;

namespace Elastic.OpenTelemetry.Processors;

/// <summary>
///
/// </summary>
public class TransactionIdProcessor : BaseProcessor<Activity>
{
    private readonly AsyncLocal<ActivitySpanId?> _currentTransactionId = new();

	/// <inheritdoc cref="OnStart"/>
    public override void OnStart(Activity activity)
    {
        if (activity.Parent == null)
            _currentTransactionId.Value = activity.SpanId;

        activity.SetTag("transaction.id", _currentTransactionId.Value);
    }
}
