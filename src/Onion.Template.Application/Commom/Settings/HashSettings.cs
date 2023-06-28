using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Commom.Settings;

public class HashSettings
{
	public const string Section = "HashSettings";
	public int Iterations { get; set; }
	public string Pepper { get; set; } = null!;
}
