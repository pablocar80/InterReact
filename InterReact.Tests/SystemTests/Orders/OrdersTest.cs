using InterReact;
using Microsoft.VisualBasic.FileIO;
using Stringification;
using System.Reactive.Linq;
namespace Orders;

public class Orders(ITestOutputHelper output, TestFixture fixture) : CollectionTestBase(output, fixture)
{
    [Fact]
    public async Task OptTest()
    {
        var sub = Client.Response.Subscribe(m => Write($"Message: {m.Stringify()}"));

        //517641765
        Contract contract = new()
        {
            ContractId =584774788,
            Exchange = "SMART",
        };

        //Client.Request.CalculateOptionPrice(123, contract, 1.00, 37);
        //Client.Request.CalculateImpliedVolatility(124, contract, 3.00, 37);
        Client.Request.RequestCompletedOrders(false);
        Client.Request.RequestExecutions(345);

        await Task.Delay(10000);

       sub.Dispose();
    }


}

