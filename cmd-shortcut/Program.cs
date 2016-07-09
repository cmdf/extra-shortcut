using System.IO;
using System.Text.RegularExpressions;
using IWshRuntimeLibrary;

namespace orez.oshortcut {
	class Program {

		// static data
		/// <summary>
		/// Windows Script Host Shell object.
		/// </summary>
		private static WshShell Sh = new WshShell();
		/// <summary>
		/// URL regex search pattern.
		/// </summary>
		private static string PatternUrl = @"[A-Za-z]+:\/\/(.+)";
		

		// static method
		/// <summary>
		/// Dream is collapsing.
		/// : Hans Zimmer
		/// </summary>
		/// <param name="args">Input arguments.</param>
		static void Main(string[] args) {
			oParams p = new oParams(args);
			if (p.TargetPath == null) return;
			Match m = Regex.Match(p.TargetPath, PatternUrl);
			bool isurl = m.Length > 0 && m.Index == 0;
			if (p.Output == null) {
				if (isurl) p.Output = m.Groups[1].Value.Replace("/", "_");
				else p.Output = Path.GetFileNameWithoutExtension(p.TargetPath.Replace(":", ""));
			}
			p.Output = ExpandPath(p.Output);
			p.TargetPath = ExpandPath(p.TargetPath);
			p.IconLocation = ExpandPath(p.IconLocation);
			p.Description = ExpandPath(p.Description);
			p.WorkingDirectory = ExpandPath(p.WorkingDirectory);
			p.Output = PathWithoutExt(p.Output) + (isurl? ".url" : ".lnk");
			if (isurl) LinkUrl(p);
			else LinkLocal(p);
		}

		/// <summary>
		/// Create local shortcut.
		/// </summary>
		/// <param name="p">Input parameters.</param>
		private static void LinkLocal(oParams p) {
			IWshShortcut l = Sh.CreateShortcut(p.Output);
			l.TargetPath = p.TargetPath;
			if (p.WindowStyle != null) l.WindowStyle = GetWindowStyle(p.WindowStyle);
			if(p.HotKey != null) l.Hotkey = p.HotKey;
			if(p.IconLocation != null) l.IconLocation = p.IconLocation;
			if(p.Description != null) l.Description = p.Description;
			if(p.WorkingDirectory != null) l.WorkingDirectory = p.WorkingDirectory;
			if (p.Arguments != null) l.Arguments = p.Arguments;
			l.Save();
		}

		/// <summary>
		/// Create URL shortcut.
		/// </summary>
		/// <param name="p">Input parameters.</param>
		private static void LinkUrl(oParams p) {
			IWshURLShortcut l = Sh.CreateShortcut(p.Output);
			l.TargetPath = p.TargetPath;
			l.Save();
		}

		/// <summary>
		/// Expand shortcut path to full path.
		/// </summary>
		/// <param name="p">Shortcut path.</param>
		/// <returns>Full path.</returns>
		private static string ExpandPath(string p) {
			if (p == null) return null;
			MatchEvaluator fn = (m) => Sh.SpecialFolders.Item(m.Groups[1].Value);
			return Regex.Replace(p, "{{(.+)}}", fn, RegexOptions.IgnoreCase);
		}

		/// <summary>
		/// Get full path without extension.
		/// </summary>
		/// <param name="p">Input path.</param>
		/// <returns>Path without extension.</returns>
		private static string PathWithoutExt(string p) {
			string d = Path.GetDirectoryName(p);
			string f = Path.GetFileNameWithoutExtension(p);
			return d + (d.Length > 0 ? "\\" : "") + f;
		}

		/// <summary>
		/// Get Window style integer from string.
		/// </summary>
		/// <param name="ws">Window style string.</param>
		/// <returns>Window style integer.</returns>
		private static int GetWindowStyle(string ws) {
			ws = ws.ToLower();
			if (ws.StartsWith("max")) return 3;
			if (ws.StartsWith("min")) return 7;
			return 1;
		}
	}
}
