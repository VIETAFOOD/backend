using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Enums
{
	public enum ShippingEnum
	{
		[Description("DefaultShippingCharge")]
		DefaultShippingCharge = 30000,

        [Description("FreeShip")]
        FreeShip = 0
    }
}
