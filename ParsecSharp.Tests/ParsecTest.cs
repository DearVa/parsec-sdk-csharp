using System.Runtime.CompilerServices;

namespace ParsecSharp.Tests;

public class ParsecTest
{
	[Test]
	public async ValueTask Test()
	{
		var authResult = await ApiTest.Auth();
		var hosts = await Api.GetHosts(new Api.GetHostsQueryParams
		{
			SessionId = authResult.SessionId,
			Mode = Api.HostMode.Desktop
		});
		
		var config = new ParsecConfig
		{
			Upnp = 1
		};
		var parsec = new Parsec(config);
		
		var clientConfig = new ClientConfig
		{
			ResolutionX = 1920,
			ResolutionY = 1080,
			RefreshRate = 60,
			Protocol = 1
		};
		
		var status = parsec.ClientConnect(clientConfig, authResult.SessionId, hosts.Data[0].PeerId);
		Assert.That(status, Is.EqualTo(Status.ParsecOk));

		unsafe
		{
			while (true)
			{
				status = parsec.ClientPollFrame(ClientPullFrameCallback, unchecked((uint)-1), IntPtr.Zero);
				if (status == Status.QueueWarnEmpty)
				{
					continue;
				}
				if (status == Status.ParsecOk)
				{
					break;
				}
			}
		}
		
		parsec.ClientDisconnect();
	}

	private unsafe static void ClientPullFrameCallback(IntPtr pFrame, byte* image, IntPtr opaque)
	{
		ref var frame = ref Unsafe.AsRef<Frame>(pFrame.ToPointer());
	}
}