namespace Elastic.OpenTelemetry.Tests;

public class TransactionIdProcessorTests
{
    [Fact]
    public void TransactionId_IsAddedToTags()
    {
        const string activitySourceName = "TestSource";

        var activitySource = new ActivitySource(activitySourceName, "1.0.0");

        var exportedItems = new List<Activity>();

        using var agent = new AgentBuilder()
            .ConfigureTracer(tpb => tpb
                .ConfigureResource(rb => rb.AddService("Test", "1.0.0"))
                .AddSource(activitySourceName)
                .AddInMemoryExporter(exportedItems))
            .Build();

        using (var activity = activitySource.StartActivity("DoingStuff", ActivityKind.Internal))
        {
            activity?.SetStatus(ActivityStatusCode.Ok);
        }

        exportedItems.Should().HaveCount(1);

        var exportedActivity = exportedItems[0];

        var transactionId = exportedActivity.GetTagItem(TransactionIdProcessor.TransactionIdTagName);

        transactionId.Should().NotBeNull().And.BeAssignableTo<string>().Which.Should().NotBeEmpty();
    }
}
