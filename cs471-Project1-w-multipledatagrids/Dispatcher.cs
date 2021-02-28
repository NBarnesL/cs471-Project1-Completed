using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs471_Project1_w_multipledatagrids
{
	public class Dispatcher
	{

		//Running process object
		public Process runningProcess = null;
		static Process previousProcess = null;

		static PriorityQueue<Process> readyQueue;

		double lastRunningID;
		private String contextSwitchInfo = "";

		//
		public Dispatcher()
		{
			readyQueue = new PriorityQueue<Process>();
		}
		//
		public String Context()
		{
			return contextSwitchInfo;
		}

		public String toString()
		{
			return readyQueue.ToString();
		}

		//Update context switch
		private void UpdateContextInfo()
		{
			String datatoadd="";
			datatoadd += "Context Switched:\n";
			datatoadd += "New Running Process:\n";
			datatoadd += runningProcess.toString();
			datatoadd += "Previous Running Process:\n"; ;
			datatoadd += previousProcess.toString();
			datatoadd += "\n\n";
			contextSwitchInfo = datatoadd;
			
		}

		

		//Add a new process to dispatcher
		public void AddProcess(Process p)
		{
			//Add to the queue
			readyQueue.Enqueue(p);
			

			//No process is running
			if (runningProcess == null)
			{
				runningProcess = readyQueue.Dequeue();
				runningProcess.start();
			}
			else
			{
				lastRunningID = runningProcess.getID();
				previousProcess = runningProcess;

				
				runningProcess.stop();

				//check if completed
				if (!runningProcess.getCompleted())
					readyQueue.Enqueue(runningProcess);

				runningProcess = readyQueue.Dequeue();
				runningProcess.start();

			}
		}

        //If process complete, return true.
        public bool checkRunning()
		{
			
				//No process is running
				if (runningProcess == null)
				{
					runningProcess = readyQueue.Dequeue();
					runningProcess.start();

					//Gantt.add(runningProcess);
					return false;
				}

				//Current running process completes
				if (runningProcess.getCompleted())
				{
					//If queue not empty
					if (readyQueue.Count() > 0)
					{
						previousProcess = runningProcess;
						//get the next process from queue
						runningProcess = readyQueue.Dequeue();

						//if queue not empty
						if (runningProcess != null)
						{
							runningProcess.start();
							UpdateContextInfo();
						}
					}
					return true;
				}

				return false;
			

			
		}

		

		public Process GetProcess()
        {
			
			Process p = runningProcess;

			return p;
        }




	}

	/*-----
	 * Priority Queue: Used for priority ordering of processes. Uses the CompareTo Function in process object
	 * to get priority and reorder depending on a bool value.
	 */
	public class PriorityQueue<Process> where Process : IComparable<Process>
	{
		private List<Process> data;
		private Process[] pist = new Process[5];

		public PriorityQueue()
		{
			this.data = new List<Process>();

		}

		public void Enqueue(Process item)
		{
			data.Add(item);
			int ci = data.Count - 1;
			while (ci > 0)
			{
				int pi = (ci - 1) / 2;
				if (data[ci].CompareTo(data[pi]) >= 0)
					break;
				Process tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
				ci = pi;
			}
		}

		public Process Dequeue()
		{
			// Assumes pq isn't empty
			int li = data.Count - 1;
			Process frontItem = data[0];
			data[0] = data[li];
			data.RemoveAt(li);

			--li;
			int pi = 0;
			while (true)
			{
				int ci = pi * 2 + 1;
				if (ci > li) break;
				int rc = ci + 1;
				if (rc <= li && data[rc].CompareTo(data[ci]) < 0)
					ci = rc;
				if (data[pi].CompareTo(data[ci]) <= 0) break;
				Process tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
				pi = ci;
			}
			return frontItem;
		}

		public override string ToString()
		{
			string s = "";
			for (int i = 0; i < data.Count; ++i)
				s += data[i].ToString() + " ";
			s += "count = " + data.Count;
			Process p = data[0];
			//p.getID();

			return s;
		}

		public int Count()
		{
			return data.Count;
		}

		public Process Peek()
		{
			Process frontItem = data[0];
			return frontItem;
		}

		public bool IsConsistent()
		{
			if (data.Count == 0) return true;
			int li = data.Count - 1; // last index
			for (int pi = 0; pi < data.Count; ++pi) // each parent index
			{
				int lci = 2 * pi + 1; // left child index
				int rci = 2 * pi + 2; // right child index
				if (lci <= li && data[pi].CompareTo(data[lci]) > 0) return false;
				if (rci <= li && data[pi].CompareTo(data[rci]) > 0) return false;
			}
			return true; // Passed all checks
		}

	}
}
