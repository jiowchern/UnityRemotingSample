using Regulus.Collection;
using Regulus.CustomType;
using Regulus.Game;
using Regulus.Remoting;
using Regulus.Utility;

using UnityEngine.SocialPlatforms.GameCenter;

internal class HostCore : ICore
{
    private Regulus.Collection.QuadTree<Player> _Map;
    private Regulus.Game.Hall _Hall;

    private Regulus.Utility.Updater _Updater;
    public HostCore()
    {
        _Updater = new Updater();
        _Map = new QuadTree<Player>(new Size(4, 4), 1000);
        _Hall = new Hall();
    }

    void ICore.AssignBinder(ISoulBinder binder)
    {
        var user = new Player(binder, _Map);
        _Hall.PushUser(user);
    }

    bool Regulus.Utility.IUpdatable.Update()
    {
        _Updater.Working();
        return true;
    }

    void Regulus.Framework.IBootable.Launch()
    {
        _Updater.Add(_Hall);
    }

    void Regulus.Framework.IBootable.Shutdown()
    {
        _Updater.Shutdown();
    }
}