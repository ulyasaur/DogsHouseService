using DogsHouseService.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Tests
{
    public class PingControllerTests
    {
        private const string PING_MESSAGE = "Dogshouseservice.Version1.0.1";

        private readonly PingController _pingController;

        public PingControllerTests()
        {
            _pingController = new PingController();
        }

        [Fact]
        public void Ping_Returns200Ok()
        {
            ActionResult<string> result = _pingController.Ping();

            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();
            result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(PING_MESSAGE);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
