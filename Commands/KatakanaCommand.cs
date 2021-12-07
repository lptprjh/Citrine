#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다

using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class KatakanaCommand : CommandBase
	{
		public override string Name => "katakana";

		public override string Usage => "/katakana <count=3>";

        public override string[] Aliases { get; } = { "katakanya" };

        public override string Description => "일본어 가타카나를 랜덤하게 출력합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			var count = 3;
			if (args.Length > 0 && !int.TryParse(args[0], out count))
				throw new CommandException();
			return string.Concat(Enumerable.Repeat(0, count).Select(_ => katakana.Random()));
		}

		readonly char[] katakana = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヰヱヲンヴガギグゲゴザジズゼゾダヂヅデドバビブベボパポプペポァィゥェォャュョヮッヵヶ".ToArray();
	}
}
