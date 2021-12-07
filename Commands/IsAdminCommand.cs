#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
    public class IsAdminCommand : CommandBase
	{
		public override string Name => "isadmin";

		public override string Usage => "/isadmin";

		public override string Description => "관리자 유무를 확인합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return sender.IsAdmin ? "yes" : "no";
		}
	}
}
