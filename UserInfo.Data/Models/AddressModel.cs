namespace UserInfo.Data.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        /// <summary>
        /// Must Be Related UserId
        /// </summary>
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
