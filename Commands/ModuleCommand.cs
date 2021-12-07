#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다

using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class ModuleCommand : CommandBase
	{
		public override string Name => "modules";

		public override string Usage => "/modules or /mods";

		public override string[] Aliases { get; } = { "mods" };

		public override string Description => "활성화되어있는 모듈을 나열합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			var mods = core.Modules.Select(mod => mod.GetType().Name);
			return $"モジュール数: {mods.Count()}\n{string.Join(",", mods)}";
		}
	}
}
