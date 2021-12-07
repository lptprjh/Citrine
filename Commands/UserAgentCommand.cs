#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Text;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class UserAgentCommand : CommandBase
	{
		public override string Name => "useragent";

		public override string Usage => "/useragent or /ua";

		public override string[] Aliases { get; } = { "ua" };

		public override string Description => "Citrine이 사용하는 HTTP Client의 User Agent를 합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return Server.Http.DefaultRequestHeaders.UserAgent.ToString();
		}
	}
}
