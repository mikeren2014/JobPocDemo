namespace JobPocDemo.Data
{
    public class JobItem
    {
        #region properties

        public string Content { get; set; } = default!;
        public int Id { get; set; }
        public string Type { get; set; } = default!;

        #endregion
    }
}
