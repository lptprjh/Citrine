#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다

using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class BalloonGenCommand : CommandBase
	{
		public override string Name => "balloongen";

		public override string Usage => "/balloongen <문자열>";

		public override string[] Aliases { get; } = { "balloon-gen", "balloon", "genballoon" };

		public override string Description => "뾰족뾰족한 말풍선을 만듭니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			body = "　" + body + "　";
			var crown = ".\n＿" + new string('人', body.Length) + "＿\n";
			var pate = "＞" + body + "＜\n";
			var heel = "￣" + string.Concat(Enumerable.Repeat("Y^", body.Length - 1)) + "Y￣";
			return crown + pate + heel;
		}
	}
}
