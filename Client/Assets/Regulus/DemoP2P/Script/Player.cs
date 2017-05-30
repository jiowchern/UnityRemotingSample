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
using Regulus.Project.RemoteDemo.Common;

internal class Player : Regulus.Game.IUser , Regulus.Collection.IQuadObject , IController , IActor
{
    private List<Player> _Controllers;
    private readonly ISoulBinder _Binder;

    private readonly QuadTree<Player> _Map;

    private float _Speed;

    private Regulus.Utility.TimeCounter _MoveTime;
    private Regulus.CustomType.Vector2 _Position;
    private Regulus.CustomType.Vector2 _Direction;

    private float _View;
    private float _Body;

    private Rect _Bounds;

    private Guid _Id;

    private float _ColorR;

    private float _ColorG;

    private float _ColorB;

    public Player(ISoulBinder binder, QuadTree<Player> map)
    {
        _Id = Guid.NewGuid();
        
        _Controllers = new List<Player>();
        _MoveTime = new TimeCounter();
        _Direction = new Regulus.CustomType.Vector2();
        _Position = new Regulus.CustomType.Vector2();
        this._Binder = binder;
        this._Map = map;

        _View = 30;
        _Body = 2;
        _ColorR = Regulus.Utility.Random.Instance.NextFloat(0, 1);
        _ColorG = Regulus.Utility.Random.Instance.NextFloat(0, 1);
        _ColorB = Regulus.Utility.Random.Instance.NextFloat(0, 1);


        _Binder.BreakEvent += _Release;
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
        _UpdateBounds();

        _UpdateBoardcast();
    }

    private void _UpdateBoardcast()
    {
        var halfView  = this._View / 2;
        var view = new Rect(this._Position.X - halfView, this._Position.Y - halfView, _View, _View);
        var controllers = _Map.Query(view);
        _Broadcast(controllers);
    }

    private void _UpdateBounds()
    {
        var halfBody = this._Body / 2;
        var bounds = new Rect(this._Position.X - halfBody, this._Position.Y - halfBody, _Body, _Body);
        if (Regulus.Utility.ValueHelper.DeepEqual(this._Bounds, bounds) == false)
        {
            this._Bounds = bounds;
            if (_BoundsChanged!= null)
                _BoundsChanged(this, new EventArgs());
            
        }
    }

    private void _UpdateMove()
    {
        _Position.X += _Speed * _Direction.X * _MoveTime.Second;
        _Position.Y += _Speed * _Direction.Y * _MoveTime.Second;
        _MoveTime.Reset();
    }

    void Regulus.Framework.IBootable.Launch()
    {
        _UpdateBounds();
        
        _Map.Insert(this);
        _Binder.Bind<IController>(this);        

    }

    void Regulus.Framework.IBootable.Shutdown()
    {
        _Binder.BreakEvent -= _Release;
        _Release();
        _Binder.Unbind<IController>(this);
    }

    private void _Release()
    {        
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
        _Speed = 1;


        var data = new MoveData()
                       {
                           VectorX = _Direction.X,
                           VectorY = _Direction.Y,
                           FirstX = _Position.X,
                           FirstY = _Position.Y,
                           Speed = _Speed
                       };
        _MoveEvent(data);
    }

    void IController.SetColor(float r, float g, float b)
    {
        _ColorR = r;
        _ColorG = g;
        _ColorB = b;
    }

    

    private event Action<MoveData> _MoveEvent;
    event Action<MoveData> IActor.MoveEvent
    {
        add
        {
            _MoveEvent += value;
        }
        remove
        {
            _MoveEvent -= value;
        }
    }
    


    void IController.Stop()
    {
        _Speed = 0;


        var data = new MoveData()
        {
            VectorX = _Direction.X,
            VectorY = _Direction.Y,
            FirstX = _Position.X,
            FirstY = _Position.Y,
            Speed = _Speed
        };

        _MoveEvent(data);
    }


 

    private void _Broadcast(IEnumerable<Player> controllers)
    {
        var current = _Controllers;

        _BroadcastJoin(controllers.Except(current));
        _BroadcastLeft(current.Except(controllers));

        _Controllers.Clear();
        _Controllers.AddRange(controllers);        
    }

    private void _BroadcastLeft(IEnumerable<Player> controllers)
    {
        foreach (var controller in controllers)
        {
            _Binder.Unbind<IActor>(controller);           
        }
    }

    private void _BroadcastJoin(IEnumerable<Player> controllers)
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


    float IActor.ColorR
    {
        get { return _ColorR; }
    }

    float IActor.ColorG
    {
        get { return _ColorG; }
    }

    float IActor.ColorB
    {
        get { return _ColorB; }
    }
}