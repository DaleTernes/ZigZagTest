using System;
using System.Collections.Generic;

namespace PubSub
{
    public class PubSubEntity : IDisposable
    {
        static readonly EventManager _eventManager = new EventManager();

        readonly Dictionary<Type, object> _subs = new Dictionary<Type, object>();
        readonly Dictionary<SimpleEvent, Action> _simpleSubs = new Dictionary<SimpleEvent, Action>();


        protected void Publish<T>( T e ) where T : IPubSubEvent
        {
            _eventManager.Publish( e );
        }


        protected void Publish( SimpleEvent e )
        {
            _eventManager.Publish( e );
        }


        internal static EventManager GetEventManager()
        {
            return _eventManager;
        }


        protected void Subscribe<T>( Action<T> action ) where T : IPubSubEvent
        {
            var type = typeof(T);
            if ( _subs.ContainsKey( type ) )
            {
                throw new Exception( "Double subscription detected! Type: " + type );
            }
            _eventManager.Subscribe( type, action );
            _subs.Add( type, action );
        }


        protected void Subscribe( SimpleEvent e, Action action )
        {
            if ( _simpleSubs.ContainsKey( e ) )
            {
                throw new Exception( "Double subscription detected! SimpleEvent: " + e );
            }
            _eventManager.Subscribe( e, action );
            _simpleSubs.Add( e, action );
        }


        protected void Unsubscribe<T>( Action<T> action ) where T : IPubSubEvent
        {
            var type = typeof(T);
            _eventManager.Unsubscribe( type, action );
            _subs.Remove( type );
        }


        protected void Unsubscribe( SimpleEvent e, Action action )
        {
            _eventManager.Unsubscribe( e, action );
            _simpleSubs.Remove( e );
        }


        protected void UnsubscribeAll()
        {
            foreach ( var pair in _subs )
                _eventManager.Unsubscribe( pair.Key, pair.Value );
            _subs.Clear();

            foreach ( var pair in _simpleSubs )
                _eventManager.Unsubscribe( pair.Key, pair.Value );
            _simpleSubs.Clear();
        }


        public virtual void Dispose()
        {
            UnsubscribeAll();
        }
    }
}