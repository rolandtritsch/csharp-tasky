using UIKit;
using Foundation;

namespace Tasky.iOS {
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {
		UIWindow window;
		UINavigationController navController;
		UITableViewController homeViewController;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
			// create a new window instance based on the screen size
			window = new UIWindow(UIScreen.MainScreen.Bounds);
			
			// make the window visible
			window.MakeKeyAndVisible();
			
			// create our nav controller
			navController = new UINavigationController();

			// create our home controller (iPhone only)
			homeViewController = new Screens.HomeScreen();

			// push the view controller onto the nav controller and show the window
			navController.PushViewController(homeViewController, false);
			window.RootViewController = navController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}