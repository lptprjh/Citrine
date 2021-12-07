#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System;
using System.Threading.Tasks;
using System.Web;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;
using Newtonsoft.Json.Linq;

namespace Citrine.Core
{
	public class NyaizeCommand : CommandBase
	{
		public override string Name => "nyaize";

		public override string Usage => "/nyaize";

		public override string[] Aliases => new []{ "nya" };

		public override string Description => "메시지를 냥체로 전환합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return body.Replace("な", "にゃ").Replace("ナ", "ニャ").Replace("ﾅ", "ﾆｬ");
		}
	}
}
