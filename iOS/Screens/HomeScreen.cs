using System.Linq;
using System.Collections.Generic;

using MonoTouch.Dialog;

using UIKit;
using Foundation;

using Tasky.Core;
using Tasky.iOS.ApplicationLayer;

namespace Tasky.iOS.Screens {
	/// <summary>
	/// A UITableViewController that uses MonoTouch.Dialog - displays the list of Tasks
	/// </summary>
	public class HomeScreen : DialogViewController {
		List<Task> tasks;
		
		// MonoTouch.Dialog individual TaskDetails view (uses /AL/TaskDialog.cs wrapper class)
		BindingContext context;
		TaskDialog taskDialog;
		Task currentTask;
		DialogViewController detailsScreen;

		public HomeScreen() : base(UITableViewStyle.Plain, null) {
			Initialize ();
		}
		
		protected void Initialize() {
			NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Add), false);
			NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowTaskDetails(new Task()); };
		}
		
		protected void ShowTaskDetails(Task task) {
			currentTask = task;
			taskDialog = new TaskDialog(task);
			context = new BindingContext(this, taskDialog, "Task Details");
			detailsScreen = new DialogViewController(context.Root, true);
			ActivateController(detailsScreen);
		}

		public void SaveTask() {
			context.Fetch (); // re-populates with updated values
			currentTask.Name = taskDialog.Name;
			currentTask.Notes = taskDialog.Notes;
			TaskManager.theDb.SaveTask(currentTask);
			NavigationController.PopViewController(true);
		}

		public void DeleteTask() {
			if (currentTask.ID >= 0) TaskManager.theDb.DeleteTask(currentTask.ID);
			NavigationController.PopViewController(true);
		}

		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			
			// reload/refresh - populate table
			tasks = TaskManager.theDb.GetTasks();
			var rows = from t in tasks
				select (Element)new StringElement((t.Name == "" ? "<new task>" : t.Name), t.Notes);
			var s = new Section();
			s.AddAll(rows);
			Root = new RootElement("Tasky") {s}; 
		}

		public override void Selected(NSIndexPath indexPath) {
			var task = tasks[indexPath.Row];
			ShowTaskDetails(task);
		}

		public override Source CreateSizingSource(bool unevenRows) {
			return new EditingSource(this);
		}

		public void DeleteTaskRow(int rowId) {
			TaskManager.theDb.DeleteTask(tasks[rowId].ID);
		}
	}
}