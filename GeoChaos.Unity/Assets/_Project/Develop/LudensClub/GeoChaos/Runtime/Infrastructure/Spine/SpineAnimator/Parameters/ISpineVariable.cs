namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public interface ISpineVariable
  {
    TParameter GetValue<TParameter>();
    void SetValue<TParameter>(TParameter value);

    object GetValue();
    void SetValue(object value);
  }
}