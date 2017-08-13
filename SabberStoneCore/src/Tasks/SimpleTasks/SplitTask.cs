﻿using SabberStoneCore.Model;
using SabberStoneCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SabberStoneCore.Tasks.SimpleTasks
{
	public class SplitTask : SimpleTask
	{
		public int Amount { get; set; }

		public EntityType Type { get; set; }

		public SplitTask(int amount, EntityType type)
		{
			Amount = amount;
			Type = type;
		}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

		public override TaskState Process()
		{
			List<IPlayable> entities = IncludeTask.GetEntites(Type, Controller, Source, Target, Playables);

			if (entities.Count == 0)
				return TaskState.STOP;

			if (Game.Splitting && Game.Splits.Count == 0)
			{
				if (Amount == 1)
				{
					entities.ForEach(p =>
					{
						//Game.Dump("SplitTask", $"{sets.IndexOf(p)}: {string.Join(";", p)}");
						Playables = new List<IPlayable> { p };
						State = TaskState.COMPLETE;
						Game clone = Game.Clone();
						Game.Splits.Add(clone);
					});
				}
				else
				{
					var sets = Util.GetPowerSet(entities).Where(p => p.Count() == Amount).ToList();
					sets.ForEach(p =>
					{
						//Game.Dump("SplitTask", $"{sets.IndexOf(p)}: {string.Join(";", p)}");
						Playables = p.ToList();
						State = TaskState.COMPLETE;
						Game clone = Game.Clone();
						Game.Splits.Add(clone);
					});
				}
			}

			Playables = entities;

			return TaskState.COMPLETE;
		}

		public override ISimpleTask InternalClone()
		{
			return new SplitTask(Amount, Type);
		}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}