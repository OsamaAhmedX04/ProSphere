namespace ProSphere.Jobs.Ban.RemoveBan
{
    public interface IRemoveBanJob
    {
        Task RemoveBan(string userId);
    }
}
