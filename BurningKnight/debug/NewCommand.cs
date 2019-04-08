using BurningKnight.entity.editor;
using BurningKnight.entity.level;
using BurningKnight.entity.level.biome;
using BurningKnight.entity.level.tile;
using Microsoft.Xna.Framework;

namespace BurningKnight.debug {
	public class NewCommand : ConsoleCommand {
		public Editor Editor;
		
		public NewCommand(Editor editor) {
			Name = "new";
			ShortName = "n";
			Editor = editor;
		}
		
		public override void Run(Console Console, string[] Args) {
			int width = 32;
			int height = 32;

			if (Args.Length == 1) {
				width = int.Parse(Args[0]);
				height = width;
			} else if (Args.Length == 2) {
				width = int.Parse(Args[0]);
				height = int.Parse(Args[1]);
			}

			foreach (var e in Editor.Area.Tags[Tags.LevelSave]) {
				e.Done = true;
			}
			
			Editor.Area.Add(Editor.Level = new RegularLevel(BiomeRegistry.Defined[Biome.Castle]) {
				Width = width, Height = height
			});

			Editor.Level.Setup();
			Editor.Level.Fill(Tiles.RandomFloor());
			Editor.Level.TileUp();
			
			Editor.Camera.Position = Vector2.Zero;
		}
	}
}