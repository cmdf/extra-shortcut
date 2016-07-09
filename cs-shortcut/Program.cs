using System.IO;
using System.Text.RegularExpressions;
using IWshRuntimeLibrary;

namespace cs_shortcut {
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
		static void Main(string[] args) {
			// args = new string[] { "C:", "-o", "{{Desktop}}\\a.url" };
			oParams p = new oParams(args);
			if (p.TargetPath == null) return;
			Match m = Regex.Match(p.TargetPath, PatternUrl);
			bool isurl = m.Length > 0 && m.Index == 0;
			if (p.Output == null) p.Output = isurl ? m.Groups[1].Value + "." : p.TargetPath.Replace(":", "");
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
			if(p.WindowStyle > 0) l.WindowStyle = p.WindowStyle;
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
			return Path.GetDirectoryName(p) + "\\" + Path.GetFileNameWithoutExtension(p);
		}
	}
}
