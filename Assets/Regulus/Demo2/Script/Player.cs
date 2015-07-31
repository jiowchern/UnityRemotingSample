using System;
using System.Linq;
using System.Collections.Generic;

using Regulus.Collection;
using Regulus.CustomType;
using Regulus.Game;
using Regulus.Remoting;
using Regulus.Utility;

using UnityEngine;

using Rect = Regulus.CustomType.Rect;

internal class Controller : Regulus.Game.IUser , Regulus.Collection.IQuadObject , IController , IActor
{
    private List<Controller> _Controllers;
    private readonly ISoulBinder _Binder;

    private readonly QuadTree<Controller> _Map;

    private int _Color;

    private float _Speed;

    private Regulus.Utility.TimeCounter _MoveTime;
    private Regulus.CustomType.Vector2 _Position;
    private Regulus.CustomType.Vector2 _Direction;

    private float _View;

    private Rect _Bounds;

    private Guid _Id;

    public Controller(ISoulBinder binder, QuadTree<Controller> map)
    {
        _Id = Guid.NewGuid();
        
        _Controllers = new List<Controller>();
        _MoveTime = new TimeCounter();
        _Direction = new Regulus.CustomType.Vector2();
        _Position = new Regulus.CustomType.Vector2();
        this._Binder = binder;
        this._Map = map;

        _View = 10;
    }

    void IUser.OnKick(Guid id)
    {
        
    }

    event OnQuit IUser.QuitEvent
    {
        add {  }	
        remove {  }
    }

    event OnNewUser IUser.VerifySuccessEvent
    {
        add {  }	
        remove {  }
    }

    bool Regulus.Utility.IUpdatable.Update()
    {
        _UpdateMove();

        _UpdateMap();
        return true;
    }

    private void _UpdateMap()
    {
        var halfView = _View / 2;
        _Bounds = new Rect(_Position.X - halfView, _Position.Y - halfView, halfView, halfView);
        if (_Speed != 0) 
            _BoundsChanged(this , EventArgs.Empty);

        var controllers = _Map.Query(_Bounds);

        _Broadcast(controllers);
    }

    

    private void _UpdateMove()
    {
        _Position.X += _Speed * _Direction.X * _MoveTime.Second;
        _Position.Y += _Speed * _Direction.Y * _MoveTime.Second;
        _MoveTime.Reset();
    }

    void Regulus.Framework.IBootable.Launch()
    {
        
        _Map.Insert(this);
        _Binder.Bind<IController>(this);        

    }

    void Regulus.Framework.IBootable.Shutdown()
    {
        
        _Binder.Unbind<IController>(this);        
        _Map.Remove(this);
    }

    Regulus.CustomType.Rect IQuadObject.Bounds
    {
        get
        {
            return _Bounds;
        }
    }


    private event EventHandler _BoundsChanged;
    event EventHandler IQuadObject.BoundsChanged
    {
        add
        {
            _BoundsChanged += value;
        }
        remove
        {
            _BoundsChanged -= value;
        }
    }

    void IController.Move(float vectorx, float vectory)
    {
        _Direction.X = vectorx;
        _Direction.Y = vectory;
        _Speed = 10;

        _MoveEvent(_Direction.X, _Direction.Y, _Speed);
    }

    void IController.Color(int rgba)
    {
        _Color = rgba;
    }

    int IActor.Color
    {
        get { return _Color; }
    }


    private event Action<float, float, float> _MoveEvent;
    event Action<float,float , float> IActor.MoveEvent
    {
        add { throw new NotImplementedException(); }
        remove { throw new NotImplementedException(); }
    }

    float IActor.Speed
    {
        get
        {
            return _Speed;
        }
    }


    void IController.Stop()
    {
        _Speed = 0;
        _MoveEvent(_Direction.X, _Direction.Y, _Speed);
    }


 

    private void _Broadcast(IEnumerable<Controller> controllers)
    {
        var current = _Controllers;

        _BroadcastJoin(controllers.Except(current));
        _BroadcastLeft(current.Except(controllers));

        _Controllers = controllers.ToList();
    }

    private void _BroadcastLeft(IEnumerable<Controller> controllers)
    {
        foreach (var controller in controllers)
        {
            _Binder.Unbind<IActor>(controller);           
        }
    }

    private void _BroadcastJoin(IEnumerable<Controller> controllers)
    {
        foreach (var controller in controllers)
        {
            _Binder.Bind<IActor>(controller);           
        }
    }

    Guid IActor.Id
    {
        get
        {
            return _Id;
        }
    }
}