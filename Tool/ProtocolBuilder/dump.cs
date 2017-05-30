   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Regulus.Project.RemoteDemo.Common.Ghost 
    { 
        public class CIActor : Regulus.Project.RemoteDemo.Common.IActor , Regulus.Remoting.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly Guid _GhostIdName;
            
            
            
            public CIActor(Guid id, bool have_return )
            {
                _HaveReturn = have_return ;
                _GhostIdName = id;            
            }
            

            Guid Regulus.Remoting.IGhost.GetID()
            {
                return _GhostIdName;
            }

            bool Regulus.Remoting.IGhost.IsReturnType()
            {
                return _HaveReturn;
            }
            object Regulus.Remoting.IGhost.GetInstance()
            {
                return this;
            }

            private event Regulus.Remoting.CallMethodCallback _CallMethodEvent;

            event Regulus.Remoting.CallMethodCallback Regulus.Remoting.IGhost.CallMethodEvent
            {
                add { this._CallMethodEvent += value; }
                remove { this._CallMethodEvent -= value; }
            }
            
            

                System.Guid _Id;
                System.Guid Regulus.Project.RemoteDemo.Common.IActor.Id { get{ return _Id;} }

                System.Single _ColorR;
                System.Single Regulus.Project.RemoteDemo.Common.IActor.ColorR { get{ return _ColorR;} }

                System.Single _ColorG;
                System.Single Regulus.Project.RemoteDemo.Common.IActor.ColorG { get{ return _ColorG;} }

                System.Single _ColorB;
                System.Single Regulus.Project.RemoteDemo.Common.IActor.ColorB { get{ return _ColorB;} }

                System.Action<Regulus.Project.RemoteDemo.Common.MoveData> _MoveEvent;
                event System.Action<Regulus.Project.RemoteDemo.Common.MoveData> Regulus.Project.RemoteDemo.Common.IActor.MoveEvent
                {
                    add { _MoveEvent += value;}
                    remove { _MoveEvent -= value;}
                }
                
            
        }
    }


    using System;  
    using System.Collections.Generic;
    
    namespace Regulus.Project.RemoteDemo.Common.Event.IActor 
    { 
        public class MoveEvent : Regulus.Remoting.IEventProxyCreator
        {

            Type _Type;
            string _Name;
            
            public MoveEvent()
            {
                _Name = "MoveEvent";
                _Type = typeof(Regulus.Project.RemoteDemo.Common.IActor);                   
            
            }
            Delegate Regulus.Remoting.IEventProxyCreator.Create(Guid soul_id,int event_id, Regulus.Remoting.InvokeEventCallabck invoke_Event)
            {                
                var closure = new Regulus.Remoting.GenericEventClosure<Regulus.Project.RemoteDemo.Common.MoveData>(soul_id , event_id , invoke_Event);                
                return new Action<Regulus.Project.RemoteDemo.Common.MoveData>(closure.Run);
            }
        

            Type Regulus.Remoting.IEventProxyCreator.GetType()
            {
                return _Type;
            }            

            string Regulus.Remoting.IEventProxyCreator.GetName()
            {
                return _Name;
            }            
        }
    }
                
   
    using System;  
    
    using System.Collections.Generic;
    
    namespace Regulus.Project.RemoteDemo.Common.Ghost 
    { 
        public class CIController : Regulus.Project.RemoteDemo.Common.IController , Regulus.Remoting.IGhost
        {
            readonly bool _HaveReturn ;
            
            readonly Guid _GhostIdName;
            
            
            
            public CIController(Guid id, bool have_return )
            {
                _HaveReturn = have_return ;
                _GhostIdName = id;            
            }
            

            Guid Regulus.Remoting.IGhost.GetID()
            {
                return _GhostIdName;
            }

            bool Regulus.Remoting.IGhost.IsReturnType()
            {
                return _HaveReturn;
            }
            object Regulus.Remoting.IGhost.GetInstance()
            {
                return this;
            }

            private event Regulus.Remoting.CallMethodCallback _CallMethodEvent;

            event Regulus.Remoting.CallMethodCallback Regulus.Remoting.IGhost.CallMethodEvent
            {
                add { this._CallMethodEvent += value; }
                remove { this._CallMethodEvent -= value; }
            }
            
            
                void Regulus.Project.RemoteDemo.Common.IController.Move(System.Single _1,System.Single _2)
                {                    

                    Regulus.Remoting.IValue returnValue = null;
                    var info = typeof(Regulus.Project.RemoteDemo.Common.IController).GetMethod("Move");
                    _CallMethodEvent(info , new object[] {_1 ,_2} , returnValue);                    
                    
                }

                
 

                void Regulus.Project.RemoteDemo.Common.IController.Stop()
                {                    

                    Regulus.Remoting.IValue returnValue = null;
                    var info = typeof(Regulus.Project.RemoteDemo.Common.IController).GetMethod("Stop");
                    _CallMethodEvent(info , new object[] {} , returnValue);                    
                    
                }

                
 

                void Regulus.Project.RemoteDemo.Common.IController.SetColor(System.Single _1,System.Single _2,System.Single _3)
                {                    

                    Regulus.Remoting.IValue returnValue = null;
                    var info = typeof(Regulus.Project.RemoteDemo.Common.IController).GetMethod("SetColor");
                    _CallMethodEvent(info , new object[] {_1 ,_2 ,_3} , returnValue);                    
                    
                }

                



            
        }
    }


            using System;  
            using System.Collections.Generic;
            
            namespace Regulus.Project.RemoteDemo{ 
                public class Protocol : Regulus.Remoting.IProtocol
                {
                    Regulus.Remoting.InterfaceProvider _InterfaceProvider;
                    Regulus.Remoting.EventProvider _EventProvider;
                    Regulus.Remoting.MemberMap _MemberMap;
                    Regulus.Serialization.ISerializer _Serializer;
                    public Protocol()
                    {
                        var types = new Dictionary<Type, Type>();
                        types.Add(typeof(Regulus.Project.RemoteDemo.Common.IActor) , typeof(Regulus.Project.RemoteDemo.Common.Ghost.CIActor) );
types.Add(typeof(Regulus.Project.RemoteDemo.Common.IController) , typeof(Regulus.Project.RemoteDemo.Common.Ghost.CIController) );
                        _InterfaceProvider = new Regulus.Remoting.InterfaceProvider(types);

                        var eventClosures = new List<Regulus.Remoting.IEventProxyCreator>();
                        eventClosures.Add(new Regulus.Project.RemoteDemo.Common.Event.IActor.MoveEvent() );
                        _EventProvider = new Regulus.Remoting.EventProvider(eventClosures);

                        _Serializer = new Regulus.Serialization.Serializer(new Regulus.Serialization.DescriberBuilder(typeof(Regulus.Project.RemoteDemo.Common.MoveData),typeof(Regulus.Remoting.ClientToServerOpCode),typeof(Regulus.Remoting.PackageCallMethod),typeof(Regulus.Remoting.PackageErrorMethod),typeof(Regulus.Remoting.PackageInvokeEvent),typeof(Regulus.Remoting.PackageLoadSoul),typeof(Regulus.Remoting.PackageLoadSoulCompile),typeof(Regulus.Remoting.PackageProtocolSubmit),typeof(Regulus.Remoting.PackageRelease),typeof(Regulus.Remoting.PackageReturnValue),typeof(Regulus.Remoting.PackageUnloadSoul),typeof(Regulus.Remoting.PackageUpdateProperty),typeof(Regulus.Remoting.RequestPackage),typeof(Regulus.Remoting.ResponsePackage),typeof(Regulus.Remoting.ServerToClientOpCode),typeof(System.Boolean),typeof(System.Byte[]),typeof(System.Byte[][]),typeof(System.Char),typeof(System.Char[]),typeof(System.Guid),typeof(System.Int32),typeof(System.Single),typeof(System.String)));


                        _MemberMap = new Regulus.Remoting.MemberMap(new[] {typeof(Regulus.Project.RemoteDemo.Common.IController).GetMethod("Move"),typeof(Regulus.Project.RemoteDemo.Common.IController).GetMethod("Stop"),typeof(Regulus.Project.RemoteDemo.Common.IController).GetMethod("SetColor")} ,new[]{ typeof(Regulus.Project.RemoteDemo.Common.IActor).GetEvent("MoveEvent") }, new [] {typeof(Regulus.Project.RemoteDemo.Common.IActor).GetProperty("Id"),typeof(Regulus.Project.RemoteDemo.Common.IActor).GetProperty("ColorR"),typeof(Regulus.Project.RemoteDemo.Common.IActor).GetProperty("ColorG"),typeof(Regulus.Project.RemoteDemo.Common.IActor).GetProperty("ColorB") }, new [] {typeof(Regulus.Project.RemoteDemo.Common.IActor),typeof(Regulus.Project.RemoteDemo.Common.IController)});
                    }

                    byte[] Regulus.Remoting.IProtocol.VerificationCode { get { return new byte[]{169,29,102,85,247,152,128,208,32,20,69,6,15,91,204,145};} }
                    Regulus.Remoting.InterfaceProvider Regulus.Remoting.IProtocol.GetInterfaceProvider()
                    {
                        return _InterfaceProvider;
                    }

                    Regulus.Remoting.EventProvider Regulus.Remoting.IProtocol.GetEventProvider()
                    {
                        return _EventProvider;
                    }

                    Regulus.Serialization.ISerializer Regulus.Remoting.IProtocol.GetSerialize()
                    {
                        return _Serializer;
                    }

                    Regulus.Remoting.MemberMap Regulus.Remoting.IProtocol.GetMemberMap()
                    {
                        return _MemberMap;
                    }
                    
                }
            }
            
