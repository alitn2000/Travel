namespace Travel.EndPoint.Api
{
    public class BaseUser
    {
        private int UserId { get; set; }

        public void SetUserId()
        {
            UserId = 1;
        }
        public int GetUserId()
        {
            return UserId;
        }
    }
}
