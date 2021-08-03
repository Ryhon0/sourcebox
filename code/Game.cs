
using Sandbox;

[Library( "browser" )]
public partial class Game : Sandbox.Game
{
	public Hud Hud { get; private set; }


	public Game()
	{
		if ( IsServer )
		{
			Log.Info( "My Gamemode Has Created Serverside!" );

			// Create a HUD entity. This entity is globally networked
			// and when it is created clientside it creates the actual
			// UI panels. You don't have to create your HUD via an entity,
			// this just feels like a nice neat way to do it.
			Hud = new Hud();
		}

		if ( IsClient )
		{
			Log.Info( "My Gamemode Has Created Clientside!" );
		}
	}

	[Event.Hotload]
	public void ReloadUI()
	{
		if ( IsServer )
		{
			Hud.Delete();
			Hud = new Hud();
		}
	}

	public override void ClientJoined( Client cl )
	{
		base.ClientJoined( cl );

		cl.Pawn = new Player();
		cl.Pawn.Camera = new FirstPersonCamera();
		cl.Pawn.Position = new Vector3( 0, 0, 20 );
	}
}
