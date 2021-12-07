#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class UnyaizeCommand : CommandBase
	{
		public override string Name => "unnyaize";

		public override string Usage => "/unnyaize";

		public override string[] Aliases => new []{ "unnya" };

		public override string Description => "냥체로 쓰인 글을 사람의 말로 바꿉니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return body.Replace("にゃ", "な").Replace("ニャ", "ナ").Replace("ﾆｬ", "ﾅ");
		}
	}
}
