#pragma warning disable CS1998 // 非同期メソッドは、'await' 演算子がないため、同期的に実行されます / 비동기 메소드는 'await' 연산자가 없어서 동기로 실행
using System;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class DumpCommand : CommandBase
	{
		public override string Name => "dump";

		public override string Usage => "/dump";

		public override string Description => "이 명령어를 포함한 게시물의 정보를, 프로그램의 표준 입출력으로 표시합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			if (sender is not PostCommandSender p)
				return "이 명령어는 유저 외에는 실행할 수 없습니다.";
			var n = p.Post;

			logger.Info($@"Dumped Post
id: {n.Id}
name: {n.User.Name ?? "NULL"}
host: {n.User.Host ?? "NULL"}
screenName: {n.User.ScreenName ?? "NULL"}
text: {n.Text ?? "NULL"}
visibility: {n.Visiblity}");
			return "이 게시물을 콘솔에 출력했습니다. 콘솔 화면을 확인해 주세요.";
		}

		private readonly Logger logger = new Logger(nameof(DumpCommand));
	}
}
