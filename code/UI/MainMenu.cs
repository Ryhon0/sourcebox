using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

[UseTemplate]
class MainMenu : Panel
{
	public static SoundEvent Hover = new SoundEvent( "ui/sounds/ui_hover.vsnd" );
	public static SoundEvent Click = new SoundEvent( "ui/sounds/ui_click.vsnd" );
	public static SoundEvent Return = new SoundEvent( "ui/sounds/ui_return.vsnd" );

	public MainMenu()
	{
		StyleSheet.Load( "/ui/MainMenu.scss" );
	}

	Window ServerBrowser;
	public void OpenServerBrowser()
	{
		if ( ServerBrowser?.Parent != null ) return;

		ServerBrowser = Window
			.With( new ServerBrowser() )
			.WithTitle( "Servers" )
			.WithMinSize( 650, 300 )
			.WithSize( 700, 500 );

		AddChild( ServerBrowser );
		ServerBrowser.Center();
	}

	Window NewGame;
	public void OpenNewGame()
	{
		if ( NewGame?.Parent != null ) return;

		NewGame = Window.With( Add.Label( "TODO: new game" ) )
			.WithTitle( "New Game" )
			.WithSize( 300, 500 )
			.WithResizable( false );

		AddChild( NewGame );
		NewGame.Center();
	}

	Window Options;
	public void OpenOptions()
	{
		if ( Options?.Parent != null ) return;

		Options = Window.With( Add.Label( "TODO: options" ) )
			.WithTitle( "Options" )
			.WithSize( 700, 500 )
			.WithResizable( false );

		AddChild( Options );
		Options.Center();
	}
}
