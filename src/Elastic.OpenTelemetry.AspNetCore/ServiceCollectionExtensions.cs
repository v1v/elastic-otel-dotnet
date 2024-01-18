// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
using Elastic.OpenTelemetry;
using Elastic.OpenTelemetry.AspNetCore;
using OpenTelemetry.Trace;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for <see cref="IServiceProvider"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds the Elastic OpenTelemetry distribution to an application via the <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="serviceCollection">TODO</param>
	/// <returns>TODO</returns>
	public static IServiceCollection AddElasticOpenTelemetryForAspNetCore(this IServiceCollection serviceCollection) =>
		new AgentBuilder().AddAspNetCore().Build(serviceCollection);

	/// <summary>
	/// TODO
	/// </summary>
	/// <param name="serviceCollection"></param>
	/// <param name="activitySourceNames"></param>
	/// <returns></returns>
	public static IServiceCollection AddElasticOpenTelemetryForAspNetCore(this IServiceCollection serviceCollection, params string[] activitySourceNames) =>
		new AgentBuilder(activitySourceNames).AddAspNetCore().Build(serviceCollection);

	/// <summary>
	/// TODO
	/// </summary>
	/// <param name="serviceCollection"></param>
	/// <param name="configureTracerProvider"></param>
	/// <returns></returns>
	public static IServiceCollection AddElasticOpenTelemetryForAspNetCore(this IServiceCollection serviceCollection, Action<TracerProviderBuilder> configureTracerProvider) =>
		new AgentBuilder().AddAspNetCore().ConfigureTracer(configureTracerProvider).Build(serviceCollection);
}
