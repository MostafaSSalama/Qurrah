namespace Qurrah.Business
{
    public class APIResult
    {
        #region Ctor
        public APIResult()
        {
            ActionResult = ActionResult.Empty;
        }
        #endregion

        #region Properties
        public ActionResult ActionResult { get; set; }
        public object Result { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        #endregion
    }
}