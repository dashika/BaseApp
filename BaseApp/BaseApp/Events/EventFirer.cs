using System;

namespace BaseApp.Events
{
	public class EventFirer
	{
		private static volatile EventFirer _instance;
		private static readonly object SyncRoot = new object();

		public static EventFirer Instance
		{
			get
			{
				if (_instance != null) return _instance;
				lock (SyncRoot)
				{
					if (_instance == null)
						_instance = new EventFirer();
				}
				return _instance;
			}
		}

		public void SubscribeAlways(string nameEvent, EventHandler<EventArgs<object>> action)
		{
			var manager = EventManager.GetWeakEventManager(this);
			manager.AddEventHandler(nameEvent, action);
		}

		public void Subscribe(string nameEvent, EventHandler<EventArgs<object>> action)
		{
			var manager = EventManager.GetWeakEventManager(this);
			if (!manager.IsExistEventHandler(nameEvent, action))
			{
				manager.AddEventHandler(nameEvent, action);
			}
		}

		public void UnSubscribe(string nameEvent, EventHandler<EventArgs<object>> action)
		{
			var manager = EventManager.GetWeakEventManager(this);
			manager.RemoveEventHandler(nameEvent, action);
		}

		public void RaiseEvent(string nameEvent)
		{
			var manager = EventManager.GetWeakEventManager(this);
			manager.RaiseEvent(this, new EventArgs(), nameEvent);
		}

		public void RaiseEvent(string nameEvent, object o)
		{
			var manager = EventManager.GetWeakEventManager(this);
			manager.RaiseEvent(this, new EventArgs<object>(o), nameEvent);
		}

		public static T Cast<T>(T typeHolder, object x)
		{
			return (T)x;
		}
	}
}
