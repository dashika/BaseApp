using Xamarin.Forms;
using BaseApp.Controls;
using BaseApp.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using System;

[assembly: ExportRenderer(typeof(GestureFrame), typeof(GestureFrameRenderer))]

namespace BaseApp.iOS.Renderers
{
	public class GestureFrameRenderer : FrameRenderer
	{
		UISwipeGestureRecognizer swipeDown;
		UISwipeGestureRecognizer swipeUp;
		UISwipeGestureRecognizer swipeLeft;
		UISwipeGestureRecognizer swipeRight;
		UITapGestureRecognizer taped;

		public GestureFrameRenderer()
		{
			MultipleTouchEnabled = true;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);
			GestureFrame control = (GestureFrame)Element;
			if (control == null)
			{
				return;
			}
			if (control.IsSwipe)
			{
				taped = new UITapGestureRecognizer(
					(action) =>
					{
						try
						{
							if (action.State == UIGestureRecognizerState.Ended)
							{
								CGPoint point = action.LocationOfTouch(0, null);
								GestureFrame _gi = (GestureFrame)this.Element;
								float scale = (float)UIScreen.MainScreen.Scale;
								_gi.OnUp(this, e, ((float)point.X) * scale, ((float)point.Y) * scale);
								_gi.OnTapped(this, e, ((float)point.X) * scale, ((float)point.Y) * scale);
							}
							else if (action.State == UIGestureRecognizerState.Began)
							{
								CGPoint point = action.LocationOfTouch(0, null);
								GestureFrame _gi = (GestureFrame)this.Element;
								float scale = (float)UIScreen.MainScreen.Scale;
								_gi.OnDown(this, e, ((float)point.X) * scale, ((float)point.Y) * scale, 0);
							}
							else if (action.State == UIGestureRecognizerState.Changed)
							{
								CGPoint point = action.LocationOfTouch(0, null);
								GestureFrame _gi = (GestureFrame)this.Element;
								float scale = (float)UIScreen.MainScreen.Scale;
								_gi.OnMove(this, e, ((float)point.X) * scale, ((float)point.Y) * scale);
							}
						}
						catch (Exception ex)
						{
							Logs.LogEx(ex);
						}
					}
				)
				{
				};

				swipeDown = new UISwipeGestureRecognizer(
					() =>
					{
						GestureFrame _gi = (GestureFrame)this.Element;
						_gi.OnSwipeDown();
					}
				)
				{
					Direction = UISwipeGestureRecognizerDirection.Down,
				};

				swipeUp = new UISwipeGestureRecognizer(
					() =>
					{
						GestureFrame _gi = (GestureFrame)this.Element;
						_gi.OnSwipeTop();
					}
				)
				{
					Direction = UISwipeGestureRecognizerDirection.Up,
				};

				swipeLeft = new UISwipeGestureRecognizer(
					() =>
					{
						GestureFrame _gi = (GestureFrame)this.Element;
						_gi.OnSwipeLeft();
					}
				)
				{
					Direction = UISwipeGestureRecognizerDirection.Left,
				};

				swipeRight = new UISwipeGestureRecognizer(
					() =>
					{
						GestureFrame _gi = (GestureFrame)this.Element;
						_gi.OnSwipeRight();
					}
				)
				{
					Direction = UISwipeGestureRecognizerDirection.Right,
				};

				if (e.NewElement == null)
				{
					if (swipeDown != null)
					{
						this.RemoveGestureRecognizer(swipeDown);
					}
					if (swipeUp != null)
					{
						this.RemoveGestureRecognizer(swipeUp);
					}
					if (swipeLeft != null)
					{
						this.RemoveGestureRecognizer(swipeLeft);
					}
					if (swipeRight != null)
					{
						this.RemoveGestureRecognizer(swipeRight);
					}
					if (taped != null)
					{
						this.RemoveGestureRecognizer(taped);
					}
				}

				if (e.OldElement == null)
				{
					this.AddGestureRecognizer(swipeDown);
					this.AddGestureRecognizer(swipeUp);
					this.AddGestureRecognizer(swipeLeft);
					this.AddGestureRecognizer(swipeRight);
					this.AddGestureRecognizer(taped);
				}
			}
		}

		private nfloat _touchesX;
		private nfloat _touchesY;

		//		private bool _resizeReady = false;
		private double _resize = 0;
		private bool ignore_action = false;

		private double DetectResizeMotion(UITouch[] arr)
		{
			//			if (_resizeReady)
			//			{
			if (arr.Length != 2)
			{
				return -1;
			}
			Console.WriteLine($"X:{arr[0].LocationInView(this).X}, Y:{arr[0].LocationInView(this).Y}");
			Console.WriteLine($"X:{arr[1].LocationInView(this).X}, Y:{arr[1].LocationInView(this).Y}");
			double resize = Math.Sqrt(Math.Abs(arr[0].LocationInView(this).X - arr[1].LocationInView(this).X) * Math.Abs(arr[0].LocationInView(this).X - arr[1].LocationInView(this).X) + Math.Abs(arr[0].LocationInView(this).Y - arr[1].LocationInView(this).Y) * Math.Abs(arr[0].LocationInView(this).Y - arr[1].LocationInView(this).Y));
			Console.WriteLine($"resize:{resize}");
			return resize;
			//			}
			//			return -1;
		}


		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			try
			{
				UITouch touch = touches.AnyObject as UITouch;
				if (touch.TapCount == 0)
				{
					var _gi = (GestureFrame)this.Element;
					var e = new EventArgs();
					var scale = (float)UIScreen.MainScreen.Scale;
					var x = ((float)touch.LocationInView(this).X) * scale;
					var y = ((float)touch.LocationInView(this).Y) * scale;

					_gi.OnUp(this, e, x, y);
					_gi.OnResizeDone(this, e);
					//					_resizeReady = false;
					//					ignore_action = true;
					ignore_action = false;
				}
			}
			catch (Exception ex)
			{
				Logs.LogEx(ex);
			}
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			try
			{
				UITouch[] arr = evt.AllTouches.ToArray<UITouch>();
				if (arr.Length == 2)
				{
					_resize = Math.Sqrt(Math.Abs(arr[0].LocationInView(this).X - arr[1].LocationInView(this).X) * Math.Abs(arr[0].LocationInView(this).X - arr[1].LocationInView(this).X) + Math.Abs(arr[0].LocationInView(this).Y - arr[1].LocationInView(this).Y) * Math.Abs(arr[0].LocationInView(this).Y - arr[1].LocationInView(this).Y));
					//					_resizeReady = true;
					ignore_action = false;
				}

				UITouch touch = touches.AnyObject as UITouch;
				if (touch != null)
				{
					_touchesX = touch.LocationInView(this).X;
					_touchesY = touch.LocationInView(this).Y;
					GestureFrame _gi = (GestureFrame)this.Element;
					EventArgs e = new EventArgs();
					_gi.OnDown(this, e, (float)_touchesX, (float)_touchesY, (int)touch.TapCount);
				}
			}
			catch (Exception ex)
			{
			
			}
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			try
			{
				GestureFrame _gi = (GestureFrame)this.Element;
				double rs = DetectResizeMotion(evt.AllTouches.ToArray<UITouch>());
				if (-1 != rs)
				{
					EventArgs e = new EventArgs();
					_gi.OnResize(this, e, _resize, rs);
				}
				else if (!ignore_action)
				{
					UITouch touch = touches.AnyObject as UITouch;
					if (touch != null)
					{
						nfloat offsetX = _touchesX - touch.LocationInView(touch.View).X;
						nfloat offsetY = _touchesY - touch.LocationInView(touch.View).Y;
						float scale = (float)UIScreen.MainScreen.Scale;

						EventArgs e = new EventArgs();
						_gi.OnMove(this, e, (float)(offsetX) / scale, (float)(offsetY) / scale);
					}
				}
			}
			catch (Exception ex)
			{
			
			}
		}
	}
}