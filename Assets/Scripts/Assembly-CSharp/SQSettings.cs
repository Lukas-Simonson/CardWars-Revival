using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using MiniJSON;
using UnityEngine;

public class SQSettings
{
	private static string cdnUrl;

	private static string manifestUrl;

	private static string serverUrl;

	private static int saveInterval;

	private static int patchingFileLimit;

	private static bool debugDisplayControllers;

	private static bool offlineMode;

	private static bool skipTutorial;

	private static bool initialized;

	private static string bundleIdentifier;

	private static string bundleVersion;

	private static string bundleShortVersion;

	public static string CDN_URL
	{
		get
		{
			return cdnUrl;
		}
	}

	public static string MANIFEST_URL
	{
		get
		{
			return manifestUrl;
		}
	}

	public static string SERVER_URL
	{
		get
		{
            string empty = string.Empty;
            string streamingAssetsFile = TFUtils.GetStreamingAssetsFile("server_settings.json");
            empty = ((!streamingAssetsFile.Contains("://")) ? File.ReadAllText(streamingAssetsFile) : getJsonPath(streamingAssetsFile));
            Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(empty);
            serverUrl = (string)dictionary["server_url"];
            return serverUrl;
		}
	}

	public static int SAVE_INTERVAL
	{
		get
		{
			return saveInterval;
		}
	}

	public static int PATCHING_FILE_LIMIT
	{
		get
		{
			return patchingFileLimit;
		}
	}

	public static string BundleIdentifier
	{
		get
		{
			return bundleIdentifier;
		}
	}

	public static string ServerURL
	{
		get
		{
			return serverUrl;
		}
	}

	public static string BundleVersion
	{
		get
		{
			return bundleVersion;
		}
	}

	public static string BundleShortVersion
	{
		get
		{
			return bundleShortVersion;
		}
	}

	// When true (server_settings.json "offline_mode"), no request is sent to the game server:
	// logins, saves, patching and version checks are answered locally. Reads the settings on first use:
	// on a fresh install the login screen asks before Session has called Init().
	public static bool OfflineMode
	{
		get
		{
			if (!initialized)
			{
				Init();
			}
			return offlineMode;
		}
	}

	// server_settings.json "skip_tutorial": turns on the debug stopTutorial flag. Reads the settings on first use.
	public static bool SkipTutorial
	{
		get
		{
			if (!initialized)
			{
				Init();
			}
			return skipTutorial;
		}
	}

	public static bool DebugDisplayControllers
	{
		get
		{
			return debugDisplayControllers;
		}
	}

	private SQSettings()
	{
	}

	private static string getJsonPath(string filePath)
	{
		WWW wWW = null;
		wWW = new WWW(filePath);
		while (!wWW.isDone)
		{
		}
		return wWW.text;
	}

	public static void Init()
	{
		TFUtils.DebugLog("Entering SQSettings Init()");
		bundleIdentifier = "com.turner.cardwars";
		string empty = string.Empty;
		string streamingAssetsFile = TFUtils.GetStreamingAssetsFile("server_settings.json");
		empty = ((!streamingAssetsFile.Contains("://")) ? File.ReadAllText(streamingAssetsFile) : getJsonPath(streamingAssetsFile));
		Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(empty);
		serverUrl = (string)dictionary["server_url"];
		offlineMode = dictionary.ContainsKey("offline_mode") && (bool)dictionary["offline_mode"];
		skipTutorial = dictionary.ContainsKey("skip_tutorial") && (bool)dictionary["skip_tutorial"];
		cdnUrl = (string)dictionary["server_url"] + "/static/";
        manifestUrl = (string)dictionary["server_url"] + "/static/manifest.json";
        streamingAssetsFile = TFUtils.GetStreamingAssetsFile("global_settings.json");
		empty = ((!streamingAssetsFile.Contains("://")) ? File.ReadAllText(streamingAssetsFile) : getJsonPath(streamingAssetsFile));
		dictionary = (Dictionary<string, object>)Json.Deserialize(empty);
		saveInterval = TFUtils.LoadInt(dictionary, "save_interval");
		object value;
		if (dictionary.TryGetValue("debugDisplayControllers", out value))
		{
			debugDisplayControllers = (bool)value;
		}
		int? num = TFUtils.TryLoadInt(dictionary, "patching_file_limit");
		if (num.HasValue)
		{
			patchingFileLimit = num.Value;
		}
		else
		{
			patchingFileLimit = 10;
		}
		initialized = true;
	}
}
