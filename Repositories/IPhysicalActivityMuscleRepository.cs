namespace healthy_lifestyle_web_app.Repositories
{
    public interface IPhysicalActivityMuscleRepository
    {
        public Task<bool> AddMuscleToPhysicalActivity(string muscleName, string activityName);
        public Task<bool> DeleteMuscleFromPhysicalActivity(string muscleName, string activityName);
    }
}
