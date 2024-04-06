using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CleanArchitectureWithDDD.RepositoriesPerformance.Repositories.InvoiceRepository;

public static class Program
{
    private static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        BenchmarkRunner.Run<GetAllCustomerInvoicesBenchmark>();
        /*
        //BenchmarkRunner.Run<ScanningBench>();
        ManualConfig config = ManualConfig.Create(DefaultConfig.Instance)
              .WithOptions(ConfigOptions.DisableOptimizationsValidator);

        // Run your benchmarks with the configured ManualConfig
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        */
    }
}
