using BurningKnight.entity.component;
using BurningKnight.ui.dialog;
using ImGuiNET;
using Lens;
using Lens.util.file;

namespace BurningKnight.level.entities {
	public class Sign : Prop {
		public string Region = "sign";
		public bool DemoOnly;
		
		public override void Save(FileWriter stream) {
			base.Save(stream);
			
			var d = GetComponent<CloseDialogComponent>(); 
			stream.WriteString(d.Variants.Length == 0 ? "" : d.Variants[0]);
			stream.WriteString(Region);
			stream.WriteBoolean(DemoOnly);
		}

		public override void Load(FileReader stream) {
			base.Load(stream);
			
			SetMessage(stream.ReadString());
			Region = stream.ReadString() ?? "sign";
			DemoOnly = stream.ReadBoolean();
		}

		public void SetMessage(string m) {
			GetComponent<CloseDialogComponent>().Variants = new [] { m };
		}
		
		public override void AddComponents() {
			base.AddComponents();

			AddComponent(new DialogComponent());
			AddComponent(new CloseDialogComponent());
			AddComponent(new ShadowComponent());
			
			GetComponent<DialogComponent>().Dialog.Voice = 30;
		}

		public override void PostInit() {
			base.PostInit();
			UpdateSprite();

			if (!Engine.EditingLevel && DemoOnly && !BK.Demo) {
				Done = true;
			}
		}

		private void UpdateSprite() {
			if (HasComponent<SliceComponent>()) {
				RemoveComponent<SliceComponent>();
			}

			var c = new SliceComponent("props", Region);
			AddComponent(c);
			
			Width = c.Sprite.Width;
			Height = c.Sprite.Height;
		}
		
		public override void RenderImDebug() {
			var d = GetComponent<CloseDialogComponent>();
			var m = d.Variants.Length == 0 ? "" : d.Variants[0];

			if (m == null) {
				m = "";
			}
			
			if (ImGui.InputText("Message", ref m, 128)) {
				SetMessage(m);
			}
			
			if (ImGui.InputText("Sprite", ref Region, 128)) {
				UpdateSprite();
			}

			ImGui.Checkbox("Demo only", ref DemoOnly);
		}
	}
}