#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
    public class ToCharCommand : CommandBase
	{
		public override string Name => "tochar";

		public override string Usage => "/tochar <data>";

		public override string Description => "16진수 배열을 문자열로 변환합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			body = Regex.Replace(body, @"\s", "");
			if (body.Length % 2 != 0)
				return "16진수 배열이 올바르지 않습니다.";

			var data = new byte[body.Length / 2];
			for (var i = 0; i < body.Length / 2; i++)
			{
				data[i] = Convert.ToByte(body.Substring(i * 2, 2), 16);
			}

			return Encoding.UTF8.GetString(data);
		}
	}
}
