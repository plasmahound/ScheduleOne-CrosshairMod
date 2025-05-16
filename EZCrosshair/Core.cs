using MelonLoader;

[assembly: MelonInfo(typeof(EZCrosshair.Core), "EZCrosshair", "1.0.0", "plasmahound", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace EZCrosshair
{
	public class Core : MelonMod
	{
		public override void OnInitializeMelon()
		{
			LoggerInstance.Msg("Initialized.");
		}
	}
}