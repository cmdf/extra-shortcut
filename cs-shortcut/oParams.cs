namespace cs_shortcut {
	class oParams {

		// data
		/// <summary>
		/// Defines output name of shortcut.
		/// </summary>
		public string Output;
		/// <summary>
		/// Defines path of the target.
		/// </summary>
		public string TargetPath;
		/// <summary>
		/// Defines window style of target.
		/// </summary>
		public int WindowStyle;
		/// <summary>
		/// Defines hot-key to start the target.
		/// </summary>
		public string HotKey;
		/// <summary>
		/// Defines icon location for target.
		/// </summary>
		public string IconLocation;
		/// <summary>
		/// Defines description of the target.
		/// </summary>
		public string Description;
		/// <summary>
		/// Defines working directory of the target.
		/// </summary>
		public string WorkingDirectory;
		/// <summary>
		/// Defines input arguments for the target.
		/// </summary>
		public string Arguments;


		// constructor
		/// <summary>
		/// Get parameters from input arguments.
		/// </summary>
		/// <param name="args">Input arguments.</param>
		public oParams(string[] args) {
			int n = 0;
			for(int i=0; i<args.Length; i++) {
				switch(args[i]) {
					case "-o":
					case "--output":
						Output = args[++i];
						break;
					case "-s":
					case "--window-style":
						int.TryParse(args[++i], out n);
						WindowStyle = n;
						break;
					case "-k":
					case "--hot-key":
						HotKey = args[++i];
						break;
					case "-i":
					case "--icon-location":
						IconLocation = args[++i];
						break;
					case "-d":
					case "--description":
						Description = args[++i];
						break;
					case "-w":
					case "--working-directory":
						WorkingDirectory = args[++i];
						break;
					case "-a":
					case "--arguments":
						Arguments = args[++i];
						break;
					default:
						TargetPath = args[i];
						break;
				}
			}
		}
	}
}
