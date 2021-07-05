﻿using SabberStoneCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SabberStoneBasicAI.AIAgents.HenryChia.BoardScore
{
	class MyAttackDamage : SabberStoneBasicAI.Score.Score
	{
		public override int Rate()
		{
			return MinionTotAtk + HeroAtk;
		}
		public override Func<List<IPlayable>, List<int>> MulliganRule()
		{
			return p => p.Where(t => t.Cost > 3).Select(t => t.Id).ToList();
		}
	}
}
