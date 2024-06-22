using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryServiceTool.Data
{
	[Serializable]
	public class Workspace
	{
		public int AppId { get; private set; }
		public string Name { get; set; }

		public Workspace(int appId, string name = "EmptyWorkspace")
		{
			AppId = appId;
			Name = name;
		}
	}
}
