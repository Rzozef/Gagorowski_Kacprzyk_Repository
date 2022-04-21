using System;
using System.Collections.Generic;
using System.Text;
using Logika;
// model ma sie komunikować z logiką, dopisać testy
namespace Model
{
    public abstract class ModelAbstractApi
    {
        protected LogikaAbstractApi _logic;
        public abstract List<Ball> GetBalls();
        public static ModelAbstractApi CreateApi(uint width, uint height)
        {
            return new ModelApi(width, height);
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        public override List<Ball> GetBalls()
        {
            return _logic.GetBalls();
        }
        internal ModelApi(uint width, uint height)
        {
            _logic = LogikaAbstractApi.CreateApi(width, height);
            _logic.CreateBalls(5); // TODO Usuń!!!
        }
    }
}
