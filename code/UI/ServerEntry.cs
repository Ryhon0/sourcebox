using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class ServerEntry : Panel
{
	public Server Server;
	public ServerEntry( Server server )
	{
		Server = server;
		if ( string.IsNullOrEmpty( Server.ServerID ) ) Add.Panel();
		else Add.Image( $"avatar:{Server.ServerID}" );
		Add.Label( Server.ServerID );
		Add.Label( Server.Players.ToString() );

		Add.Label( Server.GameName );
		Add.Label( Server.MapName );

		AddEventListener( "OnDoubleClick", () => Connect() );
	}

	public void Connect()
	{
		Log.Info( $"Connecting to {Server.ServerID}" );
		ConsoleSystem.Run( $"connect [{Server.ServerID}]" );
	}
}
