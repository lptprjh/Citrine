#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class VersionCommand : CommandBase
	{
		public override string Name => "version";

		public override string Usage => "/version or /ver or /v";

		public override string[] Aliases { get; } = { "ver", "v" };

		public override string Description => "Citrine의 버전을 가져옵니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return $"BotBone v{Server.Version} Citrine v{Const.Version}";
		}
	}
}
