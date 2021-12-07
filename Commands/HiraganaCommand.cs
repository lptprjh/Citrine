#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다

using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class HiraganaCommand : CommandBase
	{
		public override string Name => "hiragana";

		public override string Usage => "/hiragana <count=3>";

		public override string[] Aliases { get; } = { "kana", "hiraganya", "kanya" };

		public override string Description => "일본어 히라가나를 랜덤하게 출력합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			var count = 3;
			if (args.Length > 0 && !int.TryParse(args[0], out count))
				throw new CommandException();
			return string.Concat(Enumerable.Repeat(0, count).Select(_ => hiragana.Random()));
		}

		readonly char[] hiragana = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわゐゑをんゔがぎぐげござじずぜぞだぢづでどばびぶべぼぱぽぷぺぽぁぃぅぇぉゃゅょゎっ".ToArray();
	}
}
