using Sandbox.Internal;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public static class SBoxed
{
	public static async Task<Result> GetInfo( string id )
	{
		/*
		var ub = new UriBuilder( "https://proxy.sboxed.com/asset/get" );

		var query = HttpUtility.ParseQueryString( ub.Query );
		query["id"] = id;

		ub.Query = query.ToString();

		var http = new Http( ub.Uri );
		*/

		var http = new Http( new Uri( "https://proxy.sboxed.com/asset/get?id=" + id.Replace( " ", "%20" ) ) );
		var json = await http.GetStringAsync();

		return JsonSerializer.Deserialize<Result>( json );
	}
}

public class Organization
{
	[JsonPropertyName( "ident" )]
	public string Ident { get; set; }

	[JsonPropertyName( "title" )]
	public string Title { get; set; }

	[JsonPropertyName( "description" )]
	public string Description { get; set; }

	[JsonPropertyName( "thumb" )]
	public string Thumb { get; set; }

	[JsonPropertyName( "socialTwitter" )]
	public string SocialTwitter { get; set; }

	[JsonPropertyName( "socialWeb" )]
	public string SocialWeb { get; set; }
}

public class Download
{
	[JsonPropertyName( "type" )]
	public string Type { get; set; }

	[JsonPropertyName( "url" )]
	public string Url { get; set; }
}

public class Asset
{
	[JsonPropertyName( "org" )]
	public Organization Organization { get; set; }

	[JsonPropertyName( "ident" )]
	public string Id { get; set; }

	[JsonPropertyName( "title" )]
	public string Title { get; set; }

	[JsonPropertyName( "summary" )]
	public string Summary { get; set; }

	[JsonPropertyName( "description" )]
	public string Description { get; set; }

	[JsonPropertyName( "thumb" )]
	public string Thumbnail { get; set; }

	[JsonPropertyName( "background" )]
	public string Background { get; set; }

	[JsonPropertyName( "packageType" )]
	public int PackageType { get; set; }

	[JsonPropertyName( "updated" )]
	public int Updated { get; set; }

	[JsonPropertyName( "download" )]
	public Download Download { get; set; }

	[JsonPropertyName( "config" )]
	public object Config { get; set; }

	[JsonPropertyName( "usersNow" )]
	public int UsersNow { get; set; }

	[JsonPropertyName( "usersDay" )]
	public int UsersDay { get; set; }

	[JsonPropertyName( "usersMonth" )]
	public int UsersMonth { get; set; }

	[JsonPropertyName( "usersTotal" )]
	public int UsersTotal { get; set; }

	[JsonPropertyName( "tags" )]
	public List<object> Tags { get; set; }
}

public class Result
{
	[JsonPropertyName( "asset" )]
	public Asset Asset { get; set; }
}
