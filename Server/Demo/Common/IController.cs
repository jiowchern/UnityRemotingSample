
namespace Regulus.Project.RemoteDemo.Common
{
    public interface IController
    {
        void Move(float vectorx, float vectory);

        void Stop();

        void SetColor(float r, float g, float b);
    }

}
