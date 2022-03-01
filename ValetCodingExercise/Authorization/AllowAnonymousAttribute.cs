namespace ValetCodingExercise.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}
