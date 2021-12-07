#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class FujiwaraTatsuyaCommand : CommandBase
	{
		public override string Name => "fujiwaratatsuya";

		public override string Usage => "/fujiwaratatsuya";

		public override string[] Aliases => new []{ "fujiwara", "fjwr", "fujitatsu" };

		public override string Description => "テ゛キ゛ス゛ト゛を゛返゛す゛だ゛け゛の゛コ゛マ゛ン゛ド゛だ゛ぞ゛。(일본어 전용)";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return string.Concat(body.Select(c => c + "゛"));
		}
	}
}
