using System;
using System.Collections.Generic;
using System.Linq;

namespace PubSub
{
    public class EventManager
    {
        readonly Dictionary<Type, HashSet<object>> _actionsDict = new Dictionary<Type, HashSet<object>>();
        
        readonly Dictionary<SimpleEvent, HashSet<Action>> _simpleActionsDict = new Dictionary<SimpleEvent, HashSet<Action>>();

        
        public void Publish<T>( T e ) where T : IPubSubEvent
        {
            var type = typeof(T);
            HashSet<object> hashSet;
            if ( !_actionsDict.TryGetValue( type, out hashSet ) ) return;
            foreach ( Action<T> action in hashSet.OfType<Action<T>>().ToArray() )
                action( e );
        }


        public void Publish( SimpleEvent e )
        {
            HashSet<Action> hashSet;
            if ( !_simpleActionsDict.TryGetValue( e, out hashSet ) ) return;

            foreach ( var action in hashSet.ToArray() )
                action();
        }


        public void Subscribe<T>( Action<T> action ) where T : IPubSubEvent
        {
            var type = typeof(T);
            HashSet<object> hashSet;
            if ( !_actionsDict.TryGetValue( type, out hashSet ) )
            {
                hashSet = new HashSet<object>();
                _actionsDict[ type ] = hashSet;
            }
            hashSet.Add( action );
        }


        public void Subscribe( Type type, object action )
        {
            HashSet<object> hashSet;
            if ( !_actionsDict.TryGetValue( type, out hashSet ) )
            {
                hashSet = new HashSet<object>();
                _actionsDict[ type ] = hashSet;
            }
            hashSet.Add( action );
        }


        public void Subscribe( SimpleEvent e, Action action )
        {
            HashSet<Action> hashSet;
            if ( !_simpleActionsDict.TryGetValue( e, out hashSet ) )
            {
                hashSet = new HashSet<Action>();
                _simpleActionsDict[ e ] = hashSet;
            }
            hashSet.Add( action );
        }


        public void Unsubscribe<T>( Action<T> action ) where T : IPubSubEvent
        {
            var type = typeof(T);
            HashSet<object> list;
            if ( _actionsDict.TryGetValue( type, out list ) ) list.Remove( action );
        }


        public void Unsubscribe( Type type, object action )
        {
            HashSet<object> list;
            if ( _actionsDict.TryGetValue( type, out list ) ) list.Remove( action );
        }


        public void Unsubscribe( SimpleEvent e, Action action )
        {
            HashSet<Action> list;
            if ( _simpleActionsDict.TryGetValue( e, out list ) ) list.Remove( action );
        }
    }
}