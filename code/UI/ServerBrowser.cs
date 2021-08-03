using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

[UseTemplate]
class ServerBrowser : Panel
{
	public ButtonGroup List { get; set; }
	public Button ConnectButton { get; set; }
	public Button RefreshButton { get; set; }
	public TextEntry GameFilter { get; set; }
	public TextEntry MapFilter { get; set; }
	public ServerBrowser()
	{
		StyleSheet.Load( "/ui/ServerBrowser.scss" );
		_ = Refresh();
	}

	public void UpdateFilter()
	{
		foreach ( ServerEntry e in List.Children )
		{
			e.SetClass( "hidden",
				!e.Server.GameName.Contains( GameFilter.Text ?? "" ) ||
				!e.Server.MapName.Contains( MapFilter.Text ?? "" ) );
		}
	}

	public void ConnectToSelected()
	{
		var selected = List.SelectedButton as ServerEntry;
		if ( selected != null )
		{
			selected.Connect();
		}
	}

	public void RefreshSyncronously()
	{
		_ = Refresh();
	}

	public async Task Refresh()
	{
		if ( RefreshButton.HasClass( "disabled" ) ) return;

		List.DeleteChildren();
		ConnectButton.SetClass( "disabled", true );
		RefreshButton.SetClass( "disabled", true );

		Sandbox.Internal.Http client = new Sandbox.Internal.Http( new System.Uri( "http://3.143.142.133:7001/server_list" ) );
		var json = await client.GetStringAsync();
		var response = JsonSerializer.Deserialize<Servers>( json );

		foreach ( var server in response.ServerList )
		{
			var entry = new ServerEntry( server );
			List.AddChild( entry );
			entry.AddEventListener( "onclick", EntrySelected );
		}

		UpdateFilter();
		await UpdateNames();

		RefreshButton.SetClass( "disabled", false );
	}

	void EntrySelected()
	{
		ConnectButton.SetClass( "disabled", false );

		var n = List.SelectedButton.GetChild( 3 ) as Label;
		var name = n.Text;
	}

	async Task UpdateNames()
	{
		foreach ( ServerEntry e in List.Children )
		{
			var game = e.GetChild( 3 ) as Label;
			var map = e.GetChild( 4 ) as Label;

			game.Text = await GetDisplayName( e.Server.GameName );
			map.Text = await GetDisplayName( e.Server.MapName );
		}
	}

	static Dictionary<string, string> NameCache = new Dictionary<string, string>();
	public async Task<string> GetDisplayName( string id )
	{
		if ( id.StartsWith( "local." ) || id.StartsWith( "maps/" ) ) return id;
		else
		{
			if ( NameCache.ContainsKey( id ) ) return NameCache[id];
			else
			{
				try
				{
					var info = await Sandbox.Package.Fetch( id, true );
					var name = info.Title;
					Log.Info( info.Thumb );
					NameCache[id] = name;
					return name;
				}
				catch
				{
					NameCache[id] = id;
					return id;
				}
			}
		}
	}
}

public class Servers
{
	public int RefreshInterval { get; init; }
	public List<Server> ServerList { get; init; }
	public string FetchTime { get; init; }
	public bool Success { get; init; }
	public string ErrorString { get; init; }
}

public class Server
{
	public int Players { get; init; }
	public string ServerID { get; init; }
	public string GameName { get; init; }
	public string MapName { get; init; }
}
