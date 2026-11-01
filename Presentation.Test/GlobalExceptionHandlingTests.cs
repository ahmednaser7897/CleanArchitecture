using Castle.Core.Logging;
using Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Presentation.Middlewares;

namespace Presentation.Test;

public class GlobalExceptionHandlingTests
{
    [Fact]
    public async Task InvokeAsync_WithNoException_ShouldCallNextDelegate()
    {
        //arrange
        var mockLogger = new Mock<ILogger<GlobalExceptionHandling>>();
        var mockEnv = new Mock<IHostEnvironment>();
        mockEnv.Setup(e => e.EnvironmentName)
        .Returns(Environments.Development);
        var mockNext = new Mock<RequestDelegate>();
        var context = new DefaultHttpContext();
        var middleware = new GlobalExceptionHandling(mockNext.Object, mockLogger.Object, mockEnv.Object);

        //act
        await middleware.InvokeAsync(context);

        //assert
        mockNext.Verify(n => n(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithNotFoundException_ShouldSetStatusCodeTo404()
    {
        //arrange
        var mockLogger = new Mock<ILogger<GlobalExceptionHandling>>();
        var mockEnv = new Mock<IHostEnvironment>();
        mockEnv.Setup(e => e.EnvironmentName)
        .Returns(Environments.Development);

        var context = new DefaultHttpContext();
        var middleware = new GlobalExceptionHandling(_ => throw new NotFoundException("Not Found"), mockLogger.Object, mockEnv.Object);

        //act
        await middleware.InvokeAsync(context);

        //assert
        context.Response.StatusCode.Should().Be(404);
    }

}

