#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using Citrine.Core.Modules.Markov;

namespace Citrine.Core
{
	public class WakachiCommand : CommandBase
	{
		public override string Name => "wakachi";
		public override string Usage => "/wakachi <text>";
		public override string Description => "문자열에 띄어쓰기를 삽입합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return string.Join(" ", TinySegmenter.Instance.Segment(body));
		}
	}
}
