namespace Library.Models
{
    public class MyCashBox
    {
        #region Properties
        public double TotalCash { get; set; }
        #endregion

        public void AddCash(double currentPrice)
        {
            TotalCash = TotalCash + currentPrice;
        }

        public override string ToString()
        {
            return base.ToString()!;
        }
    }
}
