using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    public abstract class DaneAbstractApi
    {
        public static DaneAbstractApi CreateApi()
        {
            return new DaneApi();
        }
    }
    internal class DaneApi : DaneAbstractApi
    {

    }
}
